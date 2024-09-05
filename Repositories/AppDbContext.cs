using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<PlayerMatch> PlayerMatches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>()
                .OwnsOne(x => x.Stats, x => { x.ToTable("Stats"); });

            modelBuilder.Entity<Team>()
                .HasMany(x => x.Coaches)
                .WithOne(x => x.Team);

            modelBuilder.Entity<Team>()
                .Property(x => x.MatchesPlayed)
                .HasComputedColumnSql("([Wins] + [Losses] + [Draws])");

            modelBuilder.Entity<PlayerMatch>()
                .HasKey(x => new { x.PlayerId, x.MatchId });

            modelBuilder.Entity<PlayerMatch>()
                .HasOne(x => x.Player)
                .WithMany(x => x.PlayerMatches)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<PlayerMatch>()
                .HasOne(x => x.Match)
                .WithMany(x => x.PlayerMatches)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Match>()
                .HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeMatches)
                .HasForeignKey(x => x.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Match>()
                .HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayMatches)
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Match>()
                .HasOne(x => x.WinningTeam)
                .WithMany(x => x.WinningMatches)
                .HasForeignKey(x => x.WinningTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}