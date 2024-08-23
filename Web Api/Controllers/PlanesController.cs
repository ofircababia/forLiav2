using iFlight.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class PlanesController : ControllerBase
    {
        IFlightContext db = new IFlightContext();
        //הוספת מטוס סעיף 8.1
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("addplane")]
        public IActionResult AddPlane([FromBody] Plane plane)
        {
            try
            {
                if (plane == null)
                {
                    return BadRequest(plane);
                }
                db.Planes.Add(plane);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //מחיקת מטוס סעיף 8.2
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("deleteplane")]
        public IActionResult DeletePlane(string RegistrationCode)
        {
            try
            {
                if (RegistrationCode == null)
                {
                    return BadRequest();
                }
                Plane plane = db.Planes.Where(x => x.RegistrationCode == RegistrationCode)
                    .FirstOrDefault();
                if (plane == null)
                {
                    return NotFound($"There is no plane with registration code {RegistrationCode}");
                }
                db.Planes.Remove(plane);
                int num = db.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
