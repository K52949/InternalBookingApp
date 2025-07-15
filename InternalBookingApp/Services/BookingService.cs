using InternalBookingApp.Data;
using InternalBookingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternalBookingApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Resource)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByResourceIdAsync(int resourceId)
        {
            return await _context.Bookings
                .Include(b => b.Resource)
                .Where(b => b.ResourceId == resourceId && b.StartTime >= DateTime.Now)
                .ToListAsync();
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            if (booking.EndTime <= booking.StartTime)
                throw new Exception("End time must be after start time.");

            if (await HasConflictAsync(booking))
                throw new Exception("This resource is already booked during the requested time. Please choose another slot or resource.");

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            if (booking.EndTime <= booking.StartTime)
                throw new Exception("End time must be after start time.");

            if (await HasConflictAsync(booking))
                throw new Exception("This resource is already booked during the requested time. Please choose another slot or resource.");

            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasConflictAsync(Booking booking)
        {
            return await _context.Bookings.AnyAsync(b =>
                b.ResourceId == booking.ResourceId &&
                b.Id != booking.Id &&
                (booking.StartTime < b.EndTime && booking.EndTime > b.StartTime));
        }
    }
}