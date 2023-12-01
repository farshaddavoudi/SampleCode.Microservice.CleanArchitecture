
using Microsoft.AspNetCore.Mvc;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.App;

public class HangfireJobsController : BaseApiController
{
    private readonly IAppBackgroundJobsService _appBackgroundJobsService;

    #region ctor
    public HangfireJobsController(IAppBackgroundJobsService appBackgroundJobsService)
    {
        _appBackgroundJobsService = appBackgroundJobsService;
    }
    #endregion

    [HttpPost]
    public IActionResult AddOrUpdateSyncUsersWithRahkaranJob()
    {
        _appBackgroundJobsService.SyncUsersWithRahkaran();

        return NoContent();
    }
}