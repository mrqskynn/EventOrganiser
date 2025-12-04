using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventOrganizer.Models
{
    public class EventDetails
    {
        [Key]
        public int EventId { get; set; }

        [Required, StringLength(150)]
        public string EventName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        // Use DateTime to avoid issues with scaffolding TimeSpan
        [Required, DataType(DataType.Time)]
        public DateTime EventTime { get; set; }

        [Required, StringLength(150)]
        public string Location { get; set; }

        [Range(1, 10000)]
        public int Capacity { get; set; }

        // Navigation collection initialized
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
    }
}
