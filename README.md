# Internal Resource Booking System

## Setup
1. Clone the repository.
2. Install .NET 9.0 SDK.
3. Ensure SQL Server LocalDB is installed (included with Visual Studio).
4. Run `dotnet restore` to install dependencies.
5. Apply migrations: `dotnet ef database update`.
6. Run the app: `dotnet run`.
7. Access at `http://localhost:<port>`.

## Features
- Resource CRUD operations
- Booking creation with conflict detection
- Booking editing and deletion
- Responsive UI with Bootstrap
- ViewModels for booking creation and resource details
- Dashboard for today's bookings# InternalBookingApp
