using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.Admin;

public class BackgroundJobsController(IAppBackgroundJobsService appBackgroundJobsService) : BaseApiController
{
    [HttpPost]
    public IActionResult SetSyncUsersWithRahkaranJob()
    {
        appBackgroundJobsService.SyncUsersWithRahkaran();

        return NoContent();
    }
}