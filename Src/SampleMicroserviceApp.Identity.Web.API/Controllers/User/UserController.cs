using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Queries.GetAllUsers;
using SampleMicroserviceApp.Identity.Application.ServicesContracts;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Web.API.ActionFilter;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.User;

public class UserController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ISyncUsersService _syncUsersService;

    #region ctor
    public UserController(IMediator mediator, ISyncUsersService syncUsersService)
    {
        _mediator = mediator;
        _syncUsersService = syncUsersService;
    }
    #endregion

    [HttpPost]
    public async Task<IActionResult> SyncUsersWithRahkaran(CancellationToken cancellationToken)
    {
        await _syncUsersService.ExecuteAsync(cancellationToken);

        return NoContent();
    }

    [CustomAuthorize(Claims = new[] { nameof(ClaimConst.Identity_User_ViewAll) })]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var usersQueryable = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);

        return Ok(await usersQueryable.ToListAsync(cancellationToken));
    }
}