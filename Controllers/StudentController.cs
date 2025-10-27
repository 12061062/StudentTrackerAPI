using Microsoft.AspNetCore.Mvc;
using StudentTrackerAPI.Models;
using StudentTrackerAPI.Services;

namespace StudentTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentProcessor _processor = new StudentProcessor();

        [HttpPost("clock")]
        public IActionResult Clock([FromBody] StudentClockRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
                return BadRequest("First name is required.");

            _processor.SaveClockRecord(request);

            return Ok(new
            {
                message = "Clock event recorded successfully.",
                received = request
            });
        }
    }
}
