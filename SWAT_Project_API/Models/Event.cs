using System;
using System.ComponentModel.DataAnnotations;

namespace SWAT_Project_API.Models
{
    public class Event
    {

        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string MeetingNotes { get; set; }

        [Required(ErrorMessage = "Image name cannot be null or empty")]
        public string ImageUrl { get; set; }
    }
}
