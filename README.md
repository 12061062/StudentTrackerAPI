# StudentTrackerAPI

Before running, ensure .NET SDK 8.0+ is installed, can be downloaded at: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

In the project directory, you can run:

dotnet restore

dotnet run

If successful, you should get a message similar to this:

Now listening on: http://localhost:5118
Application started. Press Ctrl+C to shut down.

Test endpoints:

POST /api/student/clock
GET /api/professor/attendance/{name}

Where the mock database is saved:
C:\Users\<YourName>\Downloads\StudentClockRecords.csv
