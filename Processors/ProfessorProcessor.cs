using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentTrackerAPI.Services
{
    public class ProfessorProcessor
    {
        private readonly string _csvFilePath;

        public ProfessorProcessor()
        {
            // Get the same CSV file used by StudentProcessor
            string downloadsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads"
            );

            _csvFilePath = Path.Combine(downloadsPath, "StudentClockRecords.csv");

            // Ensure file exists (professor may open before any student logs in)
            if (!File.Exists(_csvFilePath))
            {
                using (var writer = new StreamWriter(_csvFilePath, append: false))
                {
                    writer.WriteLine("Timestamp,FirstName,Action,Latitude,Longitude");
                }
            }
        }

        /// <summary>
        /// Reads all attendance records from the CSV file.
        /// </summary>
        public List<AttendanceRecord> GetAllRecords()
        {
            var records = new List<AttendanceRecord>();

            var lines = File.ReadAllLines(_csvFilePath);

            // Skip header
            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');

                if (parts.Length >= 5)
                {
                    records.Add(new AttendanceRecord
                    {
                        Timestamp = parts[0],
                        FirstName = parts[1],
                        Action = parts[2],
                        Latitude = parts[3],
                        Longitude = parts[4]
                    });
                }
            }

            return records
                .OrderBy(r => r.FirstName ?? string.Empty, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        /// <summary>
        /// Returns all records for a specific student name (case-insensitive).
        /// </summary>
        public List<AttendanceRecord> GetRecordsByName(string name)
        {
            return GetAllRecords()
                .Where(r => r.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Returns records between two dates (inclusive).
        /// </summary>
        public List<AttendanceRecord> GetRecordsByDateRange(DateTime start, DateTime end)
        {
            return GetAllRecords()
                .Where(r =>
                {
                    if (DateTime.TryParse(r.Timestamp, out var ts))
                        return ts >= start && ts <= end;
                    return false;
                })
                .ToList();
        }

        /// <summary>
        /// Returns the path to the CSV file so it can be downloaded.
        /// </summary>
        public string GetCsvFilePath() => _csvFilePath;
    }

    /// <summary>
    /// Represents one attendance row.
    /// </summary>
    public class AttendanceRecord
    {
        public string Timestamp { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string Action { get; set; } = "";
        public string Latitude { get; set; } = "";
        public string Longitude { get; set; } = "";
    }
}
