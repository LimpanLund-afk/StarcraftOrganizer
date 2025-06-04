using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Data.Entities;
using Challenge = StarcraftOrganizer.Data.Entities.Challenge;

namespace StarcraftOrganizer.Data.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Map> Maps {get;set;} = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Match> Matches { get; set; } = null!;
        public DbSet<Challenge> Challenges { get; set; } = null!;
        public DbSet<ChallengeMaps> ChallengeMaps { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // CHALLENGE → PLAYER
            modelBuilder.Entity<Challenge>()
               .HasOne(c => c.Player1)
               .WithMany(p => p.ChallengesAsPlayer1)
               .HasForeignKey(c => c.Player1Id)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Challenge>()
             .HasOne(c => c.Player2)
             .WithMany(p => p.ChallengesAsPlayer2)
             .HasForeignKey(c => c.Player2Id)
             .OnDelete(DeleteBehavior.Restrict);

            // MATCH → PLAYER
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Player1)
                .WithMany()
                .HasForeignKey(m => m.Player1Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Player2)
                .WithMany()
                .HasForeignKey(m => m.Player2Id)
                .OnDelete(DeleteBehavior.NoAction);

            // MATCH → CHALLENGE
            modelBuilder.Entity<Match>()
                .HasOne(m => m.Challenge)
                .WithMany(c => c.Matches)
                .HasForeignKey(m => m.ChallengeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ChallengeMaps>()
                .HasKey(cm => new { cm.ChallengeId, cm.MapId });

            modelBuilder.Entity<ChallengeMaps>()
                .HasOne(cm => cm.Challenge)
                .WithMany(c => c.ChallengeMaps)
                .HasForeignKey(cm => cm.ChallengeId);

            modelBuilder.Entity<ChallengeMaps>()
                .HasOne(cm => cm.Map)
                .WithMany(m => m.ChallengeMaps)
                .HasForeignKey(cm => cm.MapId);
        }
    }


    
}
