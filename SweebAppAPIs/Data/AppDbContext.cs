using Microsoft.EntityFrameworkCore;
using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Models.UserInfo> Users { get; set; }
        public DbSet<Models.LoginUserResults> LoginUserResults { get; set; }
        public DbSet<Models.UserSettings> UserSettings { get; set; }
        public DbSet<Models.Devices> Devices { get; set; }
        public DbSet<Models.Rules> Rules { get; set; }
        public DbSet<Models.ThreatEvents> ThreatEvents { get; set; }
        public DbSet<Models.DetectionReasons> DetectionReasons { get; set; }
        public DbSet<Models.RuleHits> RuleHits { get; set; }
        public DbSet<Models.Alerts> Alerts { get; set; }
        public DbSet<Models.AlertsFeed> AlertFeeds { get; set; }
        public DbSet<Models.ThreatEventsWithDevice> ThreatEventsWithDevice { get; set; }
        public DbSet<Models.RuleHitsActivity> RuleHitsActivity { get; set; }
        public DbSet<Models.UserData> UserData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginUserResults>().HasNoKey();
            modelBuilder.Entity<ThreatEventsWithDevice>().HasNoKey();
            modelBuilder.Entity<RuleHitsActivity>().HasNoKey();
            modelBuilder.Entity<UserData>().HasNoKey();
            modelBuilder.Entity<AlertsFeed>().HasNoKey();
        }
    }
}
