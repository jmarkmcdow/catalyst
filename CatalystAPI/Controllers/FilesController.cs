

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
        

        [HttpPost]
        public async Task<ActionResult> CreateFile(IFormFile file)
        {
           //Validate the input. Put a limit on fielsized to avoid large upload attacks
           //Only accept .pdf by checking content type
           if (file.Length == 0 || file.Length > 20971520 || file.ContentType != "application/pdf")
           {
                return BadRequest("Invalid File");
           }
           
           // storing the file in the local directory is a bad practice.
           /* 		○ File names, content types, etc., can easily be manipulated by attackers, and they can try and upload malware or viruses
                store files in a safe location, preferably on a separate disk, in a directory without execute privileges
                Do not use the filename provided by the uploading customer. It may contain malicious data
            */
           var path = Path.Combine(Directory.GetCurrentDirectory(), $"Uploaded_file_{Guid.NewGuid()}.pdf"); 

           using (var stream = new FileStream(path, FileMode.Create))
           {
                await file.CopyToAsync(stream);
           }

           return Ok("File successfully created");
        }
    }
}