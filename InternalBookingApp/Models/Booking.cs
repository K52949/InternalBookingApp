using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternalBookingApp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int ResourceId { get; set; }

        [ForeignKey("ResourceId")]
        public Resource? Resource { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Booked by is required")]
        [StringLength(50)]
        public string BookedBy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purpose is required")]
        [StringLength(200)]
        public string Purpose { get; set; } = string.Empty;
    }
}