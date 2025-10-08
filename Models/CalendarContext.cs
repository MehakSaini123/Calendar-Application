using Microsoft.EntityFrameworkCore;
using System;

namespace Calendar.Models

{
    public class CalendarContext : DbContext
    {
        internal IEnumerable<object> events;

        public CalendarContext(DbContextOptions<CalendarContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
    }
}
