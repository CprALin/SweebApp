using Microsoft.EntityFrameworkCore;

namespace SweebAppAPIs.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Models.User> User { get; set; }
        public DbSet<Models.Device> Device { get; set; }
        public DbSet<Models.ThreatEvent> ThreatEvents { get; set; }
        public DbSet<Models.Alert> Alerts { get; set; }
    }
}
