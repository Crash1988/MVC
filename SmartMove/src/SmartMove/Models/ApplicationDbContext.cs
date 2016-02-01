using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace SmartMove.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<LeagueTeam>()
                .HasKey(t => new { t.TeamId, t.LeagueId });
            builder.Entity<LeagueTeam>()
                .HasOne(pt => pt.League)
                .WithMany(p => p.LeagueTeams)
                .HasForeignKey(pt => pt.LeagueId);

            builder.Entity<LeagueTeam>()
                .HasOne(pt => pt.Team)
                .WithMany(t => t.LeagueTeams)
                .HasForeignKey(pt => pt.TeamId);
        }
        public DbSet<Match> Match{ get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<League> League { get; set; }
        public DbSet<LeagueTeam> LeagueTeam { get; set; }
    }
}
