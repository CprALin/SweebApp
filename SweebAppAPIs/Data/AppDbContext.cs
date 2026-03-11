using Microsoft.EntityFrameworkCore;

namespace SweebAppAPIs.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Models.UserInfo> Users { get; set; }
        public DbSet<Models.LoginUserResults> LoginUserResults { get; set; }
        public DbSet<Models.UserSettings> UserSettings { get; set; }
        public DbSet<Models.Devices> Devices { get; set; }
        public DbSet<Models.Rules> Rules { get; set; }
    }
}
