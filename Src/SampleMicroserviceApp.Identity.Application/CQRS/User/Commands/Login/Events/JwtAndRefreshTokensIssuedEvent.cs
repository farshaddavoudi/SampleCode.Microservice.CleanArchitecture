using MediatR;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login.Events;

public record JwtAndRefreshTokensIssuedEvent(int UserId, string JwtToken, string RefreshToken) : INotification;