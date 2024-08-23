using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.DTO
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorPilot : ControllerBase
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? ProfilePicture { get; set; }
    }
}
