using Microsoft.AspNetCore.Mvc;
using StudentTrackerAPI.Services;

namespace StudentTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly ProfessorProcessor _processor = new ProfessorProcessor();

        // GET: /api/professor/attendance
        [HttpGet("attendance")]
        public IActionResult GetAttendance()
        {
            var records = _processor.GetAllRecords();
            return Ok(records);
        }

        [HttpGet("attendance/{name}")]
        public IActionResult GetAttendanceByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty.");

            var records = _processor.GetRecordsByName(name);
            if (records.Count == 0)
                return NotFound($"No records found for {name}.");

            return Ok(records);
        }
    }
}
