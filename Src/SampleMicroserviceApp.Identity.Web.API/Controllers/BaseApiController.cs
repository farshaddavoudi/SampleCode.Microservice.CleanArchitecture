using Microsoft.AspNetCore.Mvc;

namespace SampleMicroserviceApp.Identity.Web.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class BaseApiController : ControllerBase
{
    [NonAction]
    protected IActionResult Created(object value)
    {
        return StatusCode(StatusCodes.Status201Created, value);
    }

    [NonAction]
    protected IActionResult Created()
    {
        return StatusCode(StatusCodes.Status201Created);
    }
}