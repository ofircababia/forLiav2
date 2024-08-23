using iFlight.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]

    public class NotificationsController : ControllerBase
    {
        IFlightContext db = new IFlightContext();
        [HttpGet("ExpiredMedicalCertificates")]
        public async Task<ActionResult<IEnumerable<Pilot>>> GetPilotsWithExpiredMedical()
        {
            var expiredPilots = await db.Pilots
                .Where(p => p.MedicalExpiry < DateTime.Now)
                .ToListAsync();

            if (!expiredPilots.Any())
            {
                return NotFound("No pilots found with expired medical certificates.");
            }

            return Ok(expiredPilots);
        }

        [HttpGet("ExpiredMivhanRama")]
        public async Task<ActionResult<IEnumerable<Pilot>>> GetPilotsWithExpiredMivhanRama()
        {
            var expiredPilots = await db.Pilots
                .Where(p => p.MivhanRama < DateTime.Now)
                .ToListAsync();

            if (!expiredPilots.Any())
            {
                return NotFound("No pilots found with expired mivhan rama.");
            }

            return Ok(expiredPilots);
        }

    }
}
