using MediatR;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.KickOutUser;

public record KickOutUserCommand(int UserId) : IRequest;

// Handler
public class KickOutUserCommandHandler : IRequestHandler<KickOutUserCommand>
{
    private readonly IUserCacheService _userCacheService;

    #region ctor

    public KickOutUserCommandHandler(IUserCacheService userCacheService)
    {
        _userCacheService = userCacheService;
    }

    #endregion

    public async Task Handle(KickOutUserCommand request, CancellationToken cancellationToken)
    {
        await _userCacheService.RemoveRefreshToken(request.UserId, cancellationToken);
    }
}