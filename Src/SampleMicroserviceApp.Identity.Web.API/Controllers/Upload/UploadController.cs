namespace SampleMicroserviceApp.Identity.Web.API.Controllers.Upload;

//public class UploadController : BaseApiController
//{
//private readonly IUploaderService _uploaderService;
//private readonly ILogger<UploadController> _logger;

//#region ctor
//public UploadController(ILogger<UploadController> logger, IUploaderService uploaderService)
//{
//    _logger = logger;
//    _uploaderService = uploaderService;
//}
//#endregion

//[HttpPost("upload-file")]
//[DisableRequestSizeLimit]
//[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
//public async Task<IActionResult> UploadFile([FromForm] IFormFile file, CancellationToken cancellationToken)
//{
//    _logger.LogInformation("Start uploading the file");

//    var fileName = Request.Headers[nameof(UploadRequestData.FileName)];
//    var folderName = Request.Headers[nameof(UploadRequestData.FolderName)];
//    var subfolderName = Request.Headers[nameof(UploadRequestData.SubfolderName)];

//    var result = await _uploaderService.Upload(file, fileName, folderName, subfolderName, cancellationToken);

//    _logger.LogInformation("Finish uploading the file");

//    return Created(result);
//}
//}