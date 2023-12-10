using System.Text.Json;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.Admin;

public class AppSettingsJsonController(IConfiguration configuration, AppSettings appSettings) : BaseApiController
{
    [HttpPost]
    public IActionResult ApplyChanges()
    {
        var isDevelopment = appSettings.IsDevelopment;

        appSettings = configuration.Get<AppSettings>()!;

        appSettings.IsDevelopment = isDevelopment;

        return Ok(JsonSerializer.Serialize(appSettings));
    }
}