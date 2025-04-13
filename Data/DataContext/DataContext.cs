using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Data.Entities;
using Challenge = StarcraftOrganizer.Data.Entities.Challenge;

namespace StarcraftOrganizer.Data.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Map> Maps {get;set;} = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Match> Matches { get; set; } = null!;
        public DbSet<Challenge> Challenges { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



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

            // CHALLENGE → MAP
            modelBuilder.Entity<Challenge>()
                .HasOne(c => c.Player1VetoMap)
                .WithMany()
                .HasForeignKey(c => c.Player1VetoMapId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Challenge>()
                .HasOne(c => c.Player2VetoMap)
                .WithMany()
                .HasForeignKey(c => c.Player2VetoMapId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }


    
}
