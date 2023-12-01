using MediatR;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleUsers.Events;

public record RoleUsersChangedEvent(int RoleId, List<int> AffectedUserIds) : INotification;