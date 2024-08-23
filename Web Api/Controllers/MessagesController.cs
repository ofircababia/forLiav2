using Check.Models;
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

    public class MessagesController : ControllerBase
    {
        IFlightContext db = new IFlightContext();

        [HttpGet]
        [Route("getrequests")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public List<Message> getMessage()
        {
            return db.Messages.ToList();
        }



        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("deletemessage/{messageNumber}")]
        public IActionResult DeleteArticleSubject(int messageNumber)
        {
            try
            {
                if (messageNumber == null)
                {
                    return BadRequest();
                }
                Message message = db.Messages
                    .Where(x => x.MessageNumber == messageNumber).First();
                if (message == null)
                {
                    return NotFound($"There is no message by the number {messageNumber}");
                }
                db.Remove(message);
                db.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{messageNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMessage(int messageNumber)
        {
            var message = db.Messages.FirstOrDefault(m => m.MessageNumber == messageNumber);

            if (message == null)
            {
                return NotFound($"Message with number {messageNumber} not found.");
            }

            return Ok(message);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("addmessage")]
        public IActionResult AddMessage([FromBody] Message message)
        {
            try
            {
                if (message == null)
                {
                    return BadRequest(message);
                }
                else if (message.MessageNumber != 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                db.Messages.Add(message);
                db.SaveChanges();
                return CreatedAtAction(nameof(GetMessage), new { messageNumber = message.MessageNumber }, new
                {
                    Success = true,
                    Message = "Message added successfully.",
                    Data = message
                });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error: " + dbEx.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred: " + e.Message);
            }
        }
    }
}
