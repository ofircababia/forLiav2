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
    public class EmployeesController : ControllerBase
    {
        IFlightContext db = new IFlightContext();

        //הוספת עובד סעיף 7.1
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("addemployee")]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest(employee);
                }
                db.Employees.Add(employee);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        //מחיקת עובד סעיף 7.2
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("deleteemployee")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                Employee employee = db.Employees.Where(x => x.Id == id).FirstOrDefault();
                if (employee == null)
                {
                    return NotFound($"There is no employee with ID {id}");
                }
                db.Employees.Remove(employee);
                int num = db.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        


        //עריכת עובד סעיף 7.3
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("updateemployee")]
        public IActionResult UpdateEmployee(int Id, [FromBody] Employee updatedemployee)
        {
            try
            {
                if (updatedemployee == null)
                {
                    return BadRequest();
                }
                Employee employee = db.Employees.Where(x => x.Id == Id).First();
                if (employee == null)
                {
                    return NotFound($"Employee with ID {Id} was not found");
                }
                if (updatedemployee.FirstName != null)
                    employee.FirstName = updatedemployee.FirstName;
                if (updatedemployee.LastName != null)
                    employee.LastName = updatedemployee.LastName;
                if (updatedemployee.TelephoneNumber != null)
                    employee.TelephoneNumber = updatedemployee.TelephoneNumber;
                if (updatedemployee.PasswordKey != null)
                    employee.PasswordKey = updatedemployee.PasswordKey;
                db.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [Route("employeesignin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        // כניסת עובד למערכת סעיף 14
        public IActionResult EmployeeSignIn(int? Id, string? PasswordKey)
        {
            try
            {
                Employee employee = db.Employees
                    .Where(x => x.Id == Id && x.PasswordKey == PasswordKey)
                    .FirstOrDefault();
                if (employee == null)
                {
                    return NotFound("One of the details you entered are incorrect.");
                }
                return Ok(employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
