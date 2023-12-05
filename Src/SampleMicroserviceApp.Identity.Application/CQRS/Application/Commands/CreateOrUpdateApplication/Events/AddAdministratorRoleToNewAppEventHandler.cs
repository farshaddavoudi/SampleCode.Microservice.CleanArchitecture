using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication.Events;

public class AddAdministratorRoleToNewAppEventHandler(IRepository<RoleEntity> roleRepository) : INotificationHandler<ApplicationCreatedEvent>
{
    public async Task Handle(ApplicationCreatedEvent notification, CancellationToken cancellationToken)
    {
        RoleEntity administratorRole = new()
        {
            ApplicationId = notification.App.Id,
            Key = $"{notification.App.Key}_{nameof(Administrator)}",
            Title = $"مدیر {notification.App.Title}"
        };

        await roleRepository.AddAsync(administratorRole, cancellationToken);
    }
}