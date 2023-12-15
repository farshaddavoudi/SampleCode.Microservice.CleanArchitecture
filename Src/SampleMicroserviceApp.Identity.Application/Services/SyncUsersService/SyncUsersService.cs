using AutoMapper;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Services.SyncUsersService.Events;
using SampleMicroserviceApp.Identity.Application.ServicesContracts;
using SampleMicroserviceApp.Identity.Domain.Entities.User;
using SampleMicroserviceApp.Identity.Domain.Enums.User;
using SampleMicroserviceApp.Identity.Domain.Extensions;

namespace SampleMicroserviceApp.Identity.Application.Services.SyncUsersService;

public class SyncUsersService(
    IReadOnlyRepository<UserRahkaranViewEntity> userRahkaranViewRepository,
    IRepository<UserEntity> userRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IMediator mediator
    ) : ISyncUsersService
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // Get users from Rahkaran view 
        // Project to record type as well to be comparable
        var rahkaranUsers = await userRahkaranViewRepository.ToListProjectedAsync<UserComparableRecord>(cancellationToken);

        // Get users from Users table 
        var systemUsers = await userRepository.ToListAsync(cancellationToken);

        bool anyChange = false;

        // For each all Rahkaran users
        foreach (var rahkaranUserRecord in rahkaranUsers)
        {
            // Get the user from Users table
            var userInSystem = systemUsers.FirstOrDefault(u => u.RahkaranId == rahkaranUserRecord.RahkaranId);

            // If not any found => Create a new user with state of InitialActivation = false, then continue to next loop [User create RabbitMQ event will be produced when user initial activation has been completed]
            if (userInSystem is null)
            {
                var newUserToAdd = mapper.Map<UserEntity>(rahkaranUserRecord);

                await userRepository.AddAsync(newUserToAdd, cancellationToken);

                anyChange = true;

                continue;
            }

            // Found the user => First check the user source is Rahkaran otherwise ignore and continue the next loop. Sometimes maybe we change our Rahkaran user's source to manual to have more control over them
            if (userInSystem.UserSource != UserSource.Rahkaran)
            {
                continue;
            }

            // Convert user to a record to be compared
            UserComparableRecord systemUserRecord = mapper.Map<UserComparableRecord>(userInSystem);

            // Compare the two record types, if were the same, continue for the next loop
            if (systemUserRecord.Equals(rahkaranUserRecord))
            {
                continue;
            }

            // If not same, probably there are some changes for this user
            anyChange = true;

            UserEntity oldSystemUser = userInSystem.Clone<UserEntity>();

            // 1-  Update System user from Changed Rahkaran user
            UserEntity user = mapper.Map(rahkaranUserRecord, userInSystem);

            await userRepository.UpdateAsync(user, cancellationToken, false);

            // Publish event to reset all Caches related to this user + Produce a RabbitMQ message about the user change 
            // If the user IsRegister = true The Cache update would be one of Consumers
            await mediator.Publish(new RahkaranUserEditedEvent(rahkaranUserRecord, oldSystemUser), cancellationToken);
        }

        // Save all the changes, of course if any happened
        if (anyChange)
        {
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}