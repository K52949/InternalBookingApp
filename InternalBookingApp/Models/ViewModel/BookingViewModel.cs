using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternalBookingApp.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Resource is required")]
        public int ResourceId { get; set; }

        public SelectList? ResourceList { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Booked by is required")]
        [StringLength(50, ErrorMessage = "Booked by cannot exceed 50 characters")]
        public string BookedBy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purpose is required")]
        [StringLength(200, ErrorMessage = "Purpose cannot exceed 200 characters")]
        public string Purpose { get; set; } = string.Empty;
    }
}