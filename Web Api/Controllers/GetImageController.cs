using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class GetImageController : ControllerBase
    {

        [HttpGet]

        [Route("getprofileimage/{primaryKey}")]
        public IActionResult GetProfileImage(string primaryKey)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "profileImage");
            var file = Directory.GetFiles(imagesPath, $"{primaryKey}.*").FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            var fileType = Path.GetExtension(file).ToLower();

            // Determine the content type based on the file extension.
            var contentType = fileType switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream",
            };

            var image = System.IO.File.OpenRead(file);
            return File(image, contentType);
        }


        [HttpGet]
        [Route("getmivhanramaimage")]
        public IActionResult GetMivhanRamaImage(string primaryKey)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "mivhanramaImage");

            // Search for a file that starts with the primary key.
            var file = Directory.GetFiles(imagesPath, $"{primaryKey}.*").FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            var fileType = Path.GetExtension(file).ToLower();

            // Determine the content type based on the file extension.
            var contentType = fileType switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream",
            };

            var image = System.IO.File.OpenRead(file);
            return File(image, contentType);
        }



        [HttpGet]
        [Route("getlogbookimage")]
        public IActionResult GetLogBookImage(string primaryKey)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "logbookImage");

            // Search for a file that starts with the primary key.
            var file = Directory.GetFiles(imagesPath, $"{primaryKey}.*").FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            var fileType = Path.GetExtension(file).ToLower();

            // Determine the content type based on the file extension.
            var contentType = fileType switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream",
            };

            var image = System.IO.File.OpenRead(file);
            return File(image, contentType);
        }


        [HttpGet]
        [Route("getlicenseimage")]
        public IActionResult GetLicenseImage(string primaryKey)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "licenseImage");

            // Search for a file that starts with the primary key.
            var file = Directory.GetFiles(imagesPath, $"{primaryKey}.*").FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            var fileType = Path.GetExtension(file).ToLower();

            // Determine the content type based on the file extension.
            var contentType = fileType switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream",
            };

            var image = System.IO.File.OpenRead(file);
            return File(image, contentType);
        }


        [HttpGet]
        [Route("getidimage")]
        public IActionResult GetIdImage(string primaryKey)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "idImage");

            // Search for a file that starts with the primary key.
            var file = Directory.GetFiles(imagesPath, $"{primaryKey}.*").FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            var fileType = Path.GetExtension(file).ToLower();

            // Determine the content type based on the file extension.
            var contentType = fileType switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream",
            };

            var image = System.IO.File.OpenRead(file);
            return File(image, contentType);
        }


        [HttpGet]
        [Route("getmedicalimage")]
        public IActionResult GetMedicalImage(string primaryKey)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "medicalImage");

            // Search for a file that starts with the primary key.
            var file = Directory.GetFiles(imagesPath, $"{primaryKey}.*").FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            var fileType = Path.GetExtension(file).ToLower();

            // Determine the content type based on the file extension.
            var contentType = fileType switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream",
            };

            var image = System.IO.File.OpenRead(file);
            return File(image, contentType);
        }
    }
}
