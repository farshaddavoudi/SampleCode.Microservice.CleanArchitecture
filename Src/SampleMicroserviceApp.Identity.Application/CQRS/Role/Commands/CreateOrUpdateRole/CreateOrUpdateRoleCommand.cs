using AutoMapper;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.CreateOrUpdateRole;

public class CreateOrUpdateRoleCommand : IRequest
{
    public int? Id { get; set; }

    public int ApplicationId { get; set; }

    public string? Key { get; set; }

    public string? Title { get; set; }
}

// Handler
public class CreateOrUpdateRoleCommandHandler(
    IMapper mapper,
    IRepository<RoleEntity> roleRepository,
    IRepository<ApplicationEntity> applicationRepository
    ) : IRequestHandler<CreateOrUpdateRoleCommand>
{
    public async Task Handle(CreateOrUpdateRoleCommand request, CancellationToken cancellationToken)
    {
        if (request.Id is null) //=Add
        {
            // Get the app
            var app = await applicationRepository.GetByIdAsync(request.ApplicationId, cancellationToken);

            if (app is null)
                throw new BadRequestException("The application is invalid");

            request.Key = $"{app.Key}_{request.Key}";

            // Check the Key to be repetitive
            var isRoleWithSameKeyExists = await roleRepository.AnyAsync(
                    new RoleByKeySpec(request.Key), cancellationToken);

            if (isRoleWithSameKeyExists)
                throw new BusinessLogicException("The app key cannot be repetitive. A role with same key already exists.");

            // Insert new Role
            var roleToAdd = mapper.Map<RoleEntity>(request);

            await roleRepository.AddAsync(roleToAdd, cancellationToken);
        }
        else //=Update
        {
            // TODO
        }
    }
}