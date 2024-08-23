using iFlight.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class FieldsController : ControllerBase
    {
        IFlightContext db = new IFlightContext();
        //הוספת שדה תעופה/מנחת סעיף 15
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("addField")]
        public IActionResult AddField([FromBody] AirField field)
        {
            try
            {
                if (field == null)
                {
                    return BadRequest(field);
                }
                db.AirFields.Add(field);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getfield")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<AirField> GetField()
        {
            try
            {
                return Ok(db.AirFields.ToList());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("updatefield")]
        public IActionResult UpdateField(string icao, string name)
        {
            try
            {
                if (icao == null || name == null)
                {
                    return BadRequest();
                }
                AirField field = db.AirFields.Where(x => x.Icao == icao).First();
                if (field == null)
                {
                    return NotFound($"Field with ID {icao} was not found");
                }
                field.FieldName = name;
                db.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
