using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventOrganizer.Models;

namespace EventOrganiserBorromeoBellen.Data
{
    public class EventOrganiserBorromeoBellenContext : DbContext
    {
        public EventOrganiserBorromeoBellenContext (DbContextOptions<EventOrganiserBorromeoBellenContext> options)
            : base(options)
        {
        }

        public DbSet<EventOrganizer.Models.EventDetails> EventDetails { get; set; } = default!;
        public DbSet<EventOrganizer.Models.Participant> Participant { get; set; } = default!;
    }
}
