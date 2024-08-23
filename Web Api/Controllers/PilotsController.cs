 using iFlight.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api.DTO;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class PilotsController : ControllerBase
    {
        IFlightContext db = new IFlightContext();

        // הוספת משתמש סעיף 1 לא גמור
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("addpilot")]
        public IActionResult AddPilot([FromBody] Pilot p)
        {
            try
            {
                if (p == null)
                {
                    return BadRequest(p);

                }
                db.Add(p);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       // רשימת טייסים פתוחה סעיף 6
        [HttpGet]
        [Route("openlist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InterestedPilotsDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetList(string? flightype, int youngest, int oldest)
        {
            try
            {
                int? test = db.Pilots.Where(x => x.LicenseNumber == 5555).Select(x => x.Age1).First();
                List<InterestedPilotsDTO> pilots = db.Pilots.Include(y => y.InterestedInFlightTypes)
                    .Where(x => x.Age1 <= oldest && x.Age1 >= youngest && x.IsInterestedToBeInList == true)
                    .Where(x => x.InterestedInFlightTypes.Any(y => y.FlightType == flightype))
                    .Select
                        (
                            x => new InterestedPilotsDTO()
                            {
                                Fname = x.FirstName,
                                Lname = x.LastName,
                                PhoneNum = x.PhoneNumber,
                                Age = (int)x.Age1,
                                InterestedInFlightTypes = x.InterestedInFlightTypes
                            }
                        ).ToList();
                return Ok(pilots);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //צפיה בפרטי כל המשתמשים סעיף 9.1

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("viewallpilots")]
        public ActionResult<Pilot> viewPilots()
        {
            try
            {
                var pilots = db.Pilots
                    .Include(p => p.InterestedInFlightTypes)
                    .ThenInclude(ift => ift.FlightTypeNavigation)
                    .ToList();

                // Map to DTOs
                var pilotDtos = pilots.Select(p => new PilotDTO
                {
                    LicenseNumber = p.LicenseNumber,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Dob = p.Dob,
                    PhoneNumber = p.PhoneNumber,
                    ProfilePicture = p.ProfilePicture,
                    MedicalExpiry = p.MedicalExpiry,
                    MivhanRama = p.MivhanRama,
                    Idimage = p.Idimage,
                    LicenseImage = p.LicenseImage,
                    MedicalImage = p.MedicalImage,
                    MivhanRamaImage = p.MivhanRamaImage,
                    LogbookImage = p.LogbookImage,
                    PilotStatus = p.PilotStatus,
                    HoursToUse = p.HoursToUse,
                    LicenseType = p.LicenseType,
                    TypeRating = p.TypeRating,
                    IsInterestedToBeInList = p.IsInterestedToBeInList,
                    ID = p.ID,
                    Age1 = p.Age1,
                    InterestedInFlightTypes = p.InterestedInFlightTypes.Select(ift => new InterestedInFlightTypeDto
                    {
                        FlightTypeName = ift.FlightType,
                        LicenseNumber = ift.LicenseNumber,
                        Nothing = ift.Nothing,
                        FlightTypeNavigation = new FlightTypeDto
                        {
                            Type = ift.FlightTypeNavigation.FlightType1
                        }
                    }).ToList()
                }).ToList();

                return Ok(pilotDtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        // כניסת טייס למערכת סעיף 13
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(Pilot))]
        [Route("pilotsignin")]
        public IActionResult PilotSignIn(short LicenseNumber, int? ID)
        {
            try
            {
                Pilot pilot = db.Pilots
                    .Where(x => x.ID == ID && x.LicenseNumber == LicenseNumber).FirstOrDefault();
                if (pilot == null)
                {
                    return NotFound("One of the details you entered are incorrect.");
                }
                return Ok(pilot);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // צפיה בפרטי משתמש לפי קריטריון סעיף 9.2
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Pilot>))]
        [Route("viewpilot")]
        public IActionResult viewPilotsByCriteria(int? Id, string? FirstName, string? LastName)
        {
            try
            {
                List<Pilot> pilots;
                if (Id == null && FirstName == null)
                {
                    pilots = db.Pilots.Where(x => x.LastName == LastName).ToList();
                    if (pilots.Count != 0)
                    {
                        return Ok(pilots);
                    }
                    return NotFound("No pilot match the criteria");
                }
                else if (Id == null && LastName == null)
                {
                    pilots = db.Pilots.Where(x => x.FirstName == FirstName).ToList();
                    if (pilots.Count != 0)
                    {
                        return Ok(pilots);
                    }
                    return NotFound("No pilot match the criteria");
                }
                else if (FirstName == null && LastName == null)
                {
                    pilots = db.Pilots.Where(x => x.ID == Id).ToList();
                    if (pilots.Count!=0)
                    {
                        return Ok(pilots);
                    }
                    return NotFound("No pilot match the criteria");
                }
                else
                    return NotFound("No pilot match the criteria");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // עדכון פרטי משתמשים סעיף 9.3
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("UpdatePilot")]
        public IActionResult UpdatePilot(short LicenseNum, [FromBody] Pilot updatedpilot)
        {
            try
            {
                if (LicenseNum == null)
                {
                    return BadRequest();
                }
                Pilot pilot = db.Pilots.Include(x => x.InterestedInFlightTypes)
                .Where(x => x.LicenseNumber == LicenseNum).First();
                if (updatedpilot == null)
                {
                    return NotFound($"Pilot with license number {LicenseNum} was not found");
                }
                if (updatedpilot.FirstName != null)
                    pilot.FirstName = updatedpilot.FirstName;
                if (updatedpilot.LastName != null)
                    pilot.LastName = updatedpilot.LastName;
                if (updatedpilot.Dob != null)
                    pilot.Dob = updatedpilot.Dob;
                if (updatedpilot.PhoneNumber != null)
                    pilot.PhoneNumber = updatedpilot.PhoneNumber;
                if (updatedpilot.MedicalExpiry != null)
                    pilot.MedicalExpiry = updatedpilot.MedicalExpiry;
                if (updatedpilot.MivhanRama != null)
                    pilot.MivhanRama = updatedpilot.MivhanRama;
                if (updatedpilot.Idimage != null)
                    pilot.Idimage = updatedpilot.Idimage;
                if (updatedpilot.PilotStatus != null)
                    pilot.PilotStatus = updatedpilot.PilotStatus;
                if (updatedpilot.HoursToUse != null)
                    pilot.HoursToUse = updatedpilot.HoursToUse;
                if (updatedpilot.LicenseType != null)
                    pilot.LicenseType = updatedpilot.LicenseType;
                if (updatedpilot.TypeRating != null)
                    pilot.TypeRating = updatedpilot.TypeRating;
                if (updatedpilot.IsInterestedToBeInList != null)
                    pilot.IsInterestedToBeInList = updatedpilot.IsInterestedToBeInList;
                if (updatedpilot.ID != null)
                    pilot.ID = updatedpilot.ID;
                db.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }



        // הגדרת מדריך טיסה סעיף 16.1
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("addinstructor")]
        public IActionResult AddInstructor(InstructorDTO instructor)
        {
            try
            {
                if (instructor == null)
                {
                    return BadRequest(instructor);
                }
                FlightInstructor FI = new FlightInstructor();
                FI.LicenseNumber = instructor.LicenseNumber;
                FI.InstructorLicenseNumber = instructor.InstructorLicenseNumber;
                db.FlightInstructors.Add(FI);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        // צפייה במדריכי טיסה סעיף 16.2 לבדוק אם עובד
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("getinstructors")]
        public ActionResult<InstructorPilot> GetInstructorsList()
        {
            try
            {
                List<InstructorPilot> p = db.Pilots
                    //.Include(y => y.FlightInstructor)
                .Where(y => y.FlightInstructor != null).Select(x => new InstructorPilot
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    ProfilePicture = x.ProfilePicture
                }).ToList();
                return Ok(p);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }




        // מחיקת מדריך טיסה 16.3
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("deleteinstructor")]
        public IActionResult DeleteInstructor(short LicenseNumber)
        {
            try
            {
                if (LicenseNumber == null)
                {
                    return BadRequest();
                }
                FlightInstructor FI = db.FlightInstructors.Where(x => x.LicenseNumber == LicenseNumber).First();
                if (FI == null)
                {
                    return NotFound($"There is no Pilot by the license number {LicenseNumber}");
                }
                db.FlightInstructors.Remove(FI);
                db.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        // הוספת העדפות של טייסים סעיף 17.1
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<InterestsDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("postinterests")]
        public IActionResult PostInterest(List<InterestsDTO> interests)
        {
            try
            {
                if (interests == null || interests.Count == 0)
                {
                    return BadRequest(interests);
                }
                foreach (var item in interests)
                {
                    InterestedInFlightType IF = new InterestedInFlightType();
                    IF.LicenseNumber = item.LicenseNumber;
                    IF.FlightType = item.FlightType;
                    db.Add(IF);
                    db.SaveChanges();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // מחיקת העדפות של טייסים 17.2
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("deleteinterests")]
        public IActionResult DeleteInterest(short LicenseNumber, string interest)
        {
            try
            {
                if (LicenseNumber == null)
                {
                    return BadRequest();
                }
                InterestedInFlightType IF = db.InterestedInFlightTypes
                .Where(x => x.FlightType == interest && x.LicenseNumber == LicenseNumber).First();
                if (IF == null)
                {
                    return NotFound($"There is no Pilot by the license number {LicenseNumber}");
                }
                db.Remove(IF);
                db.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
