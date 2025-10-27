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
    }
}
