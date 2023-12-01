using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleMicroserviceApp.Identity.Application.CQRS.Claim.Commands.CreateOrUpdateClaim;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.Claim;

public class ClaimController : BaseApiController
{
    private readonly IMediator _mediator;

    #region ctor

    public ClaimController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateClaim(CreateOrUpdateClaimCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}