using InternalBookingApp.Models;
using System.Collections.Generic;

namespace InternalBookingApp.ViewModels
{
    public class ResourceDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}