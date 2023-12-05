using SampleMicroserviceApp.Identity.Application.CQRS.Claim.Commands.CreateOrUpdateClaim;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.Claim;

public class ClaimController(IMediator mediator) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateClaim(CreateOrUpdateClaimCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }
}