using HT_RESTful_Lite.Entities;
using Microsoft.EntityFrameworkCore;

namespace HT_RESTful_Lite.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<LeagueDetails> LeagueDetails { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Leagues> Leagues { get; set; }
        public DbSet<Cups> Cups { get; set; }
        public DbSet<CupDetails> CupDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeagueDetails>()
                .HasKey(c => new { c.LeagueId, c.TeamId });
            modelBuilder.Entity<CupDetails>()
                .HasKey(c => new { c.CupId, c.Round, c.LocalTeamId, c.VisitorTeamId });
        }
    }
}
