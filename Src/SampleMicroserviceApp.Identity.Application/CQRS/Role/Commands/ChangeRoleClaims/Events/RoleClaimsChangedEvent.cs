using MediatR;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleClaims.Events;

public record RoleClaimsChangedEvent(int RoleId, List<int> ClaimIds) : INotification;