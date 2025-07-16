# Internal Resource Booking System

A web application for managing internal resource bookings, built with ASP.NET Core 9.0 MVC, Entity Framework Core, and SQL Server LocalDB. Features include resource management, booking creation with conflict detection, booking editing/deletion, and a dashboard for today's bookings. The UI is responsive with Bootstrap, and the codebase demonstrates clean C#, dependency injection, and efficient LINQ queries.

## Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 with ASP.NET and web development workload
- SQL Server LocalDB (included with Visual Studio)
- Git for version control

## Setup
1. Clone the repository: `git clone <repository-url>`
2. Install dependencies: `dotnet restore`
3. Restore the database:
   - Use SQL Server Management Studio to restore `BookingApp.bak` to `(localdb)\MSSQLLocalDB`.
   - Alternatively, apply migrations: `dotnet ef database update`
4. Run the application: `dotnet run`
5. Access at `http://localhost:<port>`

## Features
- Resource CRUD operations (create, read, update, delete)
- Booking creation with conflict detection
- Booking editing and deletion with conflict checking
- Responsive UI with Bootstrap
- ViewModels for booking creation/edit and resource details
- Dashboard displaying today's bookings
