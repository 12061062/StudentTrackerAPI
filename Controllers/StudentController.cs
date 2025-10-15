using System;
using Microsoft.AspNetCore.Mvc;
using StudentTrackerAPI.Models;

namespace StudentTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        [HttpPost("clock")]
        public IActionResult Clock([FromBody] StudentClockRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName))
                return BadRequest("First name is required.");

            // Normally we’d save this to a database — for now, log it.
            Console.WriteLine($"[{DateTime.Now}] {request.FirstName} - " +
                              $"{(request.InOut == true ? "Clocked In" : "Clocked Out")} " +
                              $"at location: {request.Lat},{request.Lon}");

            return Ok(new
            {
                message = "Clock event recorded successfully.",
                received = request
            });
        }
    }
}
