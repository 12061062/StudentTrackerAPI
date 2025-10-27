using System;
using System.IO;
using StudentTrackerAPI.Models;

namespace StudentTrackerAPI.Services
{
    public class StudentProcessor
    {
        private readonly string _csvFilePath;

        public StudentProcessor()
        {
            // Get the user's Downloads folder
            string downloadsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads"
            );

            // Ensure the folder exists
            Directory.CreateDirectory(downloadsPath);

            // Create or locate the CSV file in the Downloads folder
            _csvFilePath = Path.Combine(downloadsPath, "StudentClockRecords.csv");

            // Ensure the file exists and has headers
            if (!File.Exists(_csvFilePath))
            {
                using (var writer = new StreamWriter(_csvFilePath, append: false))
                {
                    writer.WriteLine("Timestamp,FirstName,Action,Latitude,Longitude");
                }
            }
        }

        public void SaveClockRecord(StudentClockRequest request)
        {
            try
            {
                string action = request.InOut == true ? "Clock In" : "Clock Out";

                string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}," +
                              $"{Sanitize(request.FirstName)}," +
                              $"{action}," +
                              $"{request.Lat?.ToString("F5") ?? "N/A"}," +
                              $"{request.Lon?.ToString("F5") ?? "N/A"}";

                File.AppendAllText(_csvFilePath, line + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StudentProcessor] Error saving record: {ex.Message}");
                throw;
            }
        }

        private static string Sanitize(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            return value.Replace(",", "").Replace("\r", "").Replace("\n", "");
        }
    }
}
