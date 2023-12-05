using SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.Queries.GetAllApplications;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.Application;

public class ApplicationController(IMediator mediator) : BaseApiController
{
    [CustomAuthorize(Claims = new[] { nameof(ClaimConst.Identity_Application_Manage) })]
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateApplication(CreateOrUpdateApplicationCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [CustomAuthorize(Claims = new[] { nameof(ClaimConst.Identity_Application_ViewAll) })]
    [HttpGet]
    public async Task<IActionResult> GetAllApplications(CancellationToken cancellationToken)
    {
        var allApps = await mediator.Send(new GetAllApplicationsQuery(), cancellationToken);

        return Ok(allApps);
    }
}