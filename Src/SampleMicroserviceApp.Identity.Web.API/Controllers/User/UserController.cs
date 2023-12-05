using Microsoft.EntityFrameworkCore;
using SampleMicroserviceApp.Identity.Application.CQRS.User.Queries.GetAllUsers;
using SampleMicroserviceApp.Identity.Application.ServicesContracts;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.User;

public class UserController(IMediator mediator, ISyncUsersService syncUsersService) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> SyncUsersWithRahkaran(CancellationToken cancellationToken)
    {
        await syncUsersService.ExecuteAsync(cancellationToken);

        return NoContent();
    }

    [CustomAuthorize(Claims = new[] { nameof(ClaimConst.Identity_User_ViewAll) })]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var usersQueryable = await mediator.Send(new GetAllUsersQuery(), cancellationToken);

        return Ok(await usersQueryable.ToListAsync(cancellationToken));
    }
}