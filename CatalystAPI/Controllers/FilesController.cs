

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CatalsytAPI.Controllers{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        public FilesController( FileExtensionContentTypeProvider fectp)
        {
            _fileExtensionContentTypeProvider = fectp ?? throw new System.ArgumentNullException(nameof (fectp));
        }


        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            fileId = "../Heavy-Duty-Workbench-Plan.pdf";

            if(!System.IO.File.Exists(fileId))
                return NotFound();

            // if media type can't be determined
            if (!_fileExtensionContentTypeProvider.TryGetContentType(fileId, out var contentType))
            {
                // set content type to default media type for binary data
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(fileId);    
            return File(bytes, contentType, Path.GetFileName(fileId));
        }
    }
}