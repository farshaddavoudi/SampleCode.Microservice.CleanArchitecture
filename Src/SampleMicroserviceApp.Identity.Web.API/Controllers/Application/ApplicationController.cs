using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.Queries.GetAllApplications;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Web.API.ActionFilter;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.Application;

public class ApplicationController : BaseApiController
{
    private readonly IMediator _mediator;

    #region ctor

    public ApplicationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    [CustomAuthorize(Claims = new[] { nameof(ClaimConst.Identity_Application_Manage) })]
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateApplication(CreateOrUpdateApplicationCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [AllowAnonymous]
    //[CustomAuthorize(Claims = new[] { nameof(ClaimConst.Identity_Application_ViewAll) })]
    [HttpGet]
    public async Task<IActionResult> GetAllApplications(CancellationToken cancellationToken)
    {
        var allApps = await _mediator.Send(new GetAllApplicationsQuery(), cancellationToken);

        return Ok(allApps);
    }
}