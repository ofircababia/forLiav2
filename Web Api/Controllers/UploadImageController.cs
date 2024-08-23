using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System;
using System.Linq;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using iFlight;
using iFlight.Models;
using Microsoft.AspNetCore.Cors;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class FileUploadController : ControllerBase
    {
        IFlightContext db = new IFlightContext();

        [HttpPost("uploadprofileimage")]
        public async Task<IActionResult> UploadProfileImage(IFormFile file, [FromForm] short LicenseNumber)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var ProfilePath = Path.Combine(Directory.GetCurrentDirectory(), "profileImage"); //שם התקייה

            if (!Directory.Exists(ProfilePath))
            {
                Directory.CreateDirectory(ProfilePath);
            }
            var fileName = string.IsNullOrEmpty(LicenseNumber.ToString()) // שומר את זה בתור 8941.png
                ? file.FileName
                : $"{LicenseNumber}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(ProfilePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Pilot pilot = db.Pilots.Where(x => x.LicenseNumber == LicenseNumber).First();
            pilot.ProfilePicture = "~/profileImage/" + fileName;
            db.SaveChanges();

            return Ok(new { fileName = fileName, filePath = filePath });
        }


        [HttpPost("uploadlicenseimage")]

        public async Task<IActionResult> UploadLicesneImage(IFormFile file, [FromForm] short LicenseNumber)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var LicensePath = Path.Combine(Directory.GetCurrentDirectory(), "licenseImage");

            if (!Directory.Exists(LicensePath))
            {
                Directory.CreateDirectory(LicensePath);
            }

            // Use the custom file name provided by the client
            var fileName = string.IsNullOrEmpty(LicenseNumber.ToString()) // שומר את זה בתור 8941.png
                ? file.FileName
                : $"{LicenseNumber}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(LicensePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Pilot pilot = db.Pilots.Where(x => x.LicenseNumber == LicenseNumber).First();
            pilot.LicenseImage = "~/licenseImage/" + fileName;
            db.SaveChanges();

            return Ok(new { fileName = fileName, filePath = filePath });
        }


        [HttpPost("uploadidimage")]

        public async Task<IActionResult> UploadIDImage(IFormFile file, [FromForm] short LicenseNumber)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var LicensePath = Path.Combine(Directory.GetCurrentDirectory(), "idImage");

            if (!Directory.Exists(LicensePath))
            {
                Directory.CreateDirectory(LicensePath);
            }

            // Use the custom file name provided by the client
            var fileName = string.IsNullOrEmpty(LicenseNumber.ToString()) // שומר את זה בתור 8941.png
                ? file.FileName
                : $"{LicenseNumber}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(LicensePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Pilot pilot = db.Pilots.Where(x => x.LicenseNumber == LicenseNumber).First();
            pilot.Idimage = "~/idImage/" + fileName;
            db.SaveChanges();

            return Ok(new { fileName = fileName, filePath = filePath });
        }

        [HttpPost("uploadmedicalimage")]

        public async Task<IActionResult> UploadMedicalImage(IFormFile file, [FromForm] short LicenseNumber)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var LicensePath = Path.Combine(Directory.GetCurrentDirectory(), "medicalImage");

            if (!Directory.Exists(LicensePath))
            {
                Directory.CreateDirectory(LicensePath);
            }

            // Use the custom file name provided by the client
            var fileName = string.IsNullOrEmpty(LicenseNumber.ToString()) // שומר את זה בתור 8941.png
                ? file.FileName
                : $"{LicenseNumber}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(LicensePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Pilot pilot = db.Pilots.Where(x => x.LicenseNumber == LicenseNumber).First();
            pilot.MedicalImage = "~/medicalImage/" + fileName;
            db.SaveChanges();

            return Ok(new { fileName = fileName, filePath = filePath });
        }

        [HttpPost("uploadmivhanramaimage")]

        public async Task<IActionResult> UploadivhanRamaImage(IFormFile file, [FromForm] short LicenseNumber)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var LicensePath = Path.Combine(Directory.GetCurrentDirectory(), "mivhanramaImage");

            if (!Directory.Exists(LicensePath))
            {
                Directory.CreateDirectory(LicensePath);
            }

            // Use the custom file name provided by the client
            var fileName = string.IsNullOrEmpty(LicenseNumber.ToString()) // שומר את זה בתור 8941.png
                ? file.FileName
                : $"{LicenseNumber}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(LicensePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Pilot pilot = db.Pilots.Where(x => x.LicenseNumber == LicenseNumber).First();
            pilot.MivhanRamaImage = "~/mivhanramaImage/" + fileName;
            db.SaveChanges();

            return Ok(new { fileName = fileName, filePath = filePath });
        }


        [HttpPost("uploadlogbookimage")]
        public async Task<IActionResult> UploadLogBookImage(IFormFile file, [FromForm] short LicenseNumber)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var LicensePath = Path.Combine(Directory.GetCurrentDirectory(), "logbookImage");

            if (!Directory.Exists(LicensePath))
            {
                Directory.CreateDirectory(LicensePath);
            }

            // Use the custom file name provided by the client
            var fileName = string.IsNullOrEmpty(LicenseNumber.ToString()) // שומר את זה בתור 8941.png
                ? file.FileName
                : $"{LicenseNumber}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(LicensePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Pilot pilot = db.Pilots.Where(x => x.LicenseNumber == LicenseNumber).First();
            pilot.LogbookImage = "~/logbookImage/" + fileName;
            db.SaveChanges();

            return Ok(new { fileName = fileName, filePath = filePath });
        }
    }

}
