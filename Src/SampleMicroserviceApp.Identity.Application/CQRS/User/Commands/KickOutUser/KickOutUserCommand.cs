using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.KickOutUser;

public record KickOutUserCommand(int UserId) : IRequest;

// Handler
public class KickOutUserCommandHandler(IUserCacheService userCacheService) : IRequestHandler<KickOutUserCommand>
{
    public async Task Handle(KickOutUserCommand request, CancellationToken cancellationToken)
    {
        await userCacheService.RemoveRefreshToken(request.UserId, cancellationToken);
    }
}