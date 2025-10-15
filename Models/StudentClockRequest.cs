using System;

namespace StudentTrackerAPI.Models
{
    public class StudentClockRequest
    {
        public string? FirstName { get; set; }
        public bool? InOut { get; set; }     // true=in, false=out
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
