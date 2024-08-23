using Azure.Core;
using iFlight.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Web_Api.Classes;
using Web_Api.DTO;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class SlotsController : ControllerBase
    {
        IFlightContext db = new IFlightContext();

        //צפיה ביומן המטוס סעיף 2.1
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("getallslots")]
        public ActionResult<Slot> GetAllSlots()
        {
            try
            {
                return Ok(db.Slots.ToList());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }


        // עדכון פרטי סלוט 2.2
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("updateslot")]
        public IActionResult UpdateSlot(int FlightNumber, int DHour, int DMinute, int LHour, int LMinute, [FromBody] Slot updatedSlot)
        {
            try
            {
                if (FlightNumber == null)
                {
                    return BadRequest();
                }
                Slot slot = db.Slots.Where(x => x.FlightNumber == FlightNumber).First();
                if (slot == null)
                {
                    return NotFound($"Slot with ID {FlightNumber} was not found");
                }
                if (updatedSlot.FlightDate != null)
                    slot.FlightDate = updatedSlot.FlightDate;
                if (updatedSlot.StartHobbs != null)
                    slot.StartHobbs = updatedSlot.StartHobbs;
                if (updatedSlot.EndHobbs != null)
                    slot.EndHobbs = updatedSlot.EndHobbs;
                if (updatedSlot.Tach != null)
                    slot.Tach = updatedSlot.Tach;
                if (DHour != null && DMinute != null)
                {
                    slot.DepartTime = new TimeSpan(DHour, DMinute, 0);
                }
                if (LHour != null && LMinute != null)
                {
                    slot.LandingTime = new TimeSpan(LHour, LMinute, 0);
                }
                if (updatedSlot.FuelAmount != null)
                    slot.FuelAmount = updatedSlot.FuelAmount;
                if (updatedSlot.NumOfPassengers != null)
                    slot.NumOfPassengers = updatedSlot.NumOfPassengers;
                if (updatedSlot.DepartingStrip != null)
                    slot.DepartingStrip = updatedSlot.DepartingStrip;
                if (updatedSlot.LandingStrip != null)
                    slot.LandingStrip = updatedSlot.LandingStrip;
                if (updatedSlot.IntermediateLandingStrip != null)
                    slot.IntermediateLandingStrip = updatedSlot.IntermediateLandingStrip;
                if (updatedSlot.PilotLicenseNumber != null)
                    slot.PilotLicenseNumber = updatedSlot.PilotLicenseNumber;
                if (updatedSlot.InstructorLicenseNumber != null)
                    slot.InstructorLicenseNumber = updatedSlot.InstructorLicenseNumber;
                if (updatedSlot.RegistrationCode != null)
                    slot.RegistrationCode = updatedSlot.RegistrationCode;
                if (updatedSlot.EndHobbs != null && updatedSlot.StartHobbs != null)
                {
                    Pilot pilot = db.Pilots.Where(x => x.LicenseNumber == slot.PilotLicenseNumber).First();
                    updatedSlot.TotalHobbs = updatedSlot.EndHobbs - updatedSlot.StartHobbs;
                    pilot.HoursToUse -= slot.TotalHobbs;
                    pilot.HoursToUse += updatedSlot.TotalHobbs;
                    slot.TotalHobbs = updatedSlot.EndHobbs - updatedSlot.StartHobbs;
                }
                db.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        // צפיה בטיסות עבר לפי מס רישיון 3
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PastFlightsDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("pastflights")]
        public IActionResult GetPastFlights(int license)
        {
            try
            {
                DateTime today = DateTime.Today;
                List<PastFlightsDTO> lst = db.Slots
                    .Where(x => x.PilotLicenseNumber == license && x.FlightDate < today)
                    .Select(x => new PastFlightsDTO()
                    {
                        flightDate = (DateTime)x.FlightDate,
                        DepartingStrip = x.DepartingStrip,
                        LandingStrip = x.LandingStrip,
                        IntermediateLandingStrip = x.IntermediateLandingStrip,
                        TotalHobbs = x.TotalHobbs
                    }).ToList();
                if (lst.Count==0)
                {
                    return NotFound("No past flight for this pilot");
                }
                return Ok(lst);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //הוספת סלוט סעיף 4
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Slot))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("addslot")]
        public IActionResult AddSlot(int DHour, int DMinute, int LHour, int LMinute, [FromBody] Slot slot)
        {
            try
            {
                if (slot == null)
                {
                    return BadRequest("Slot data cannot be null.");
                }

                if (slot.FlightNumber != 0)
                {
                    return BadRequest("Flight number must be 0 for a new slot.");
                }

                slot.TotalHobbs = slot.EndHobbs - slot.StartHobbs;

                slot.DepartTime = new TimeSpan(DHour, DMinute, 0);
                slot.LandingTime = new TimeSpan(LHour, LMinute, 0);

                db.Add(slot);

                if (slot.EndHobbs.HasValue && slot.StartHobbs.HasValue)
                {
                    int? pilotLicense = slot.PilotLicenseNumber;
                    Pilot pilot = db.Pilots.FirstOrDefault(x => x.LicenseNumber == pilotLicense);

                    if (pilot == null)
                    {
                        return BadRequest($"Pilot with license number {pilotLicense} not found.");
                    }

                    pilot.HoursToUse -= slot.TotalHobbs ?? 0;
                }

                db.SaveChanges();
                return Ok(slot); // Return the created slot or a success message
            }
            catch (Exception e)
            {
                // Log the exception details here
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {e.Message}");
            }
        }


        // צפיה בטיסות עתידיות סעיף 10
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FutureSlotDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("futureflights")]

        public IActionResult GetFutureSlots(int licenseNum)
        {
            try
            {
                DateTime today = DateTime.Today;
                List<FutureSlotDTO> lst = db.Slots
                    .Where(x => x.PilotLicenseNumber == licenseNum && x.FlightDate >= today)
                    .Select(x =>
                            new FutureSlotDTO()
                            {
                                Departingtime = x.DepartTime,
                                Landingtime = x.LandingTime,
                                Flightdate = x.FlightDate
                            }
                            ).ToList();
                if (lst.Count == 0)
                {
                    return NotFound("No future flight for this pilot");
                }
                return Ok(lst);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
