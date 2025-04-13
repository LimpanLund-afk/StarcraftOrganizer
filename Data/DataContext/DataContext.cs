using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Data.Entities;

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
    }
}
