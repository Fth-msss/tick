using Microsoft.EntityFrameworkCore;
using tick.Server.Models;


namespace tick.Server
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Event> Event { get; set; }
        public DbSet<PhysicalLayout> PhysicalLayouts { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Seatlock> Seatlock { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<User> User { get; set; }
    }
}
