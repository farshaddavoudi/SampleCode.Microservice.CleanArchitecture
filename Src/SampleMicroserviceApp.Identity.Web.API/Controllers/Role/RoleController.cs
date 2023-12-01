using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleClaims;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleUsers;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.CreateOrUpdateRole;
using SampleMicroserviceApp.Identity.Application.CQRS.Role.Queries.GetAllRoles;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Web.API.ActionFilter;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.Role;

public class RoleController : BaseApiController
{
    private readonly IMediator _mediator;

    #region ctor

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    [CustomAuthorize(Claims = new[] { ClaimConst.Identity_Role_Manage })]
    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateRole(CreateOrUpdateRoleCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [CustomAuthorize(Claims = new[] { ClaimConst.Identity_Role_Manage })]
    [HttpPost]
    public async Task<IActionResult> ChangeRoleUsers(ChangeRoleUsersCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [CustomAuthorize(Claims = new[] { ClaimConst.Identity_Role_Manage })]
    [HttpPost]
    public async Task<IActionResult> ChangeRoleClaims(ChangeRoleClaimsCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [CustomAuthorize(Claims = new[] { nameof(ClaimConst.Identity_Role_ViewAll) })]
    [HttpGet]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var rolesQueryable = await _mediator.Send(new GetAllRolesQuery(), cancellationToken);

        return Ok(await rolesQueryable.ToListAsync(cancellationToken));
    }
}