using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.App;

public class HangfireJobsController(IAppBackgroundJobsService appBackgroundJobsService) : BaseApiController
{
    [HttpPost]
    public IActionResult AddOrUpdateSyncUsersWithRahkaranJob()
    {
        appBackgroundJobsService.SyncUsersWithRahkaran();

        return NoContent();
    }
}