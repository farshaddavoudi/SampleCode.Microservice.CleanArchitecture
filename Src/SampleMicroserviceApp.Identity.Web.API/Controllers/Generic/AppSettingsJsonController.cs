using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers.App;

public class AppSettingsJsonController : BaseApiController
{
    private readonly IConfiguration _configuration;
    private AppSettings? _appSettings;

    #region ctor

    public AppSettingsJsonController(IConfiguration configuration, AppSettings appSettings)
    {
        _configuration = configuration;
        _appSettings = appSettings;
    }

    #endregion

    [HttpPost]
    public IActionResult ApplyChanges()
    {
        var isDevelopment = _appSettings!.IsDevelopment;

        _appSettings = _configuration.Get<AppSettings>();

        _appSettings!.IsDevelopment = isDevelopment;

        return Ok(JsonSerializer.Serialize(_appSettings));
    }
}