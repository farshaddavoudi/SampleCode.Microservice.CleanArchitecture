using MediatR;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication.Events;

public class AddAdministratorRoleToNewAppEventHandler : INotificationHandler<ApplicationCreatedEvent>
{
    private readonly IRepository<RoleEntity> _roleRepository;

    #region ctor

    public AddAdministratorRoleToNewAppEventHandler(IRepository<RoleEntity> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    #endregion

    public async Task Handle(ApplicationCreatedEvent notification, CancellationToken cancellationToken)
    {
        RoleEntity administratorRole = new()
        {
            ApplicationId = notification.App.Id,
            Key = $"{notification.App.Key}_{nameof(Administrator)}",
            Title = $"مدیر {notification.App.Title}"
        };

        await _roleRepository.AddAsync(administratorRole, cancellationToken);
    }
}