using System;
using System.ComponentModel.DataAnnotations;

namespace EventOrganizer.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress, StringLength(120)]
        public string Email { get; set; }

        [Required, StringLength(20)]
        public string ContactNumber { get; set; }

        // Foreign key to Event
        [Range(1, int.MaxValue, ErrorMessage = "Select an event")] // require selecting a valid event id
        public int EventId { get; set; }

        // Make navigation property nullable to avoid implicit required validation
        public EventDetails? Event { get; set; }

        // JoinedDate only (date part)
        [Required, DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }
    }
}
