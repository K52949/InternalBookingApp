using InternalBookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternalBookingApp.Services
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllBookingsAsync();
        Task<List<Booking>> GetBookingsByResourceIdAsync(int resourceId);
        Task CreateBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking); // Add this
        Task<bool> HasConflictAsync(Booking booking);
    }
}