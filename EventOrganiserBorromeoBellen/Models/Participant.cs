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
        [Required]
        public int EventId { get; set; }

        public EventDetails Event { get; set; }

        // JoinedDate only (date part)
        [Required, DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }
    }
}
