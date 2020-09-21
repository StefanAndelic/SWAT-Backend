using System;
using System.ComponentModel.DataAnnotations;

namespace SWAT_Project_API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        [Required]
        public DateTime BookingTime { get; set; }
        public string Note { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
    }
}
