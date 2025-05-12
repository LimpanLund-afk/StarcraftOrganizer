using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StarcraftOrganizer.Data.DataContext;
using StarcraftOrganizer.Data.Entities;

namespace StarcraftOrganizer.Services
{

    public class PlayerService : BaseService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public PlayerService(IDbContextFactory<DataContext> factory, IDbContextFactory<DataContext> dbContextFactory) : base(factory)
        {
            _contextFactory = dbContextFactory;
        }

        public async Task<List<Player>> GetAllPlayersAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Players.ToListAsync();
        }

        public async Task<Player?> GetPlayerByIdAsync(string id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Players
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Player>> SearchPlayersByNameAsync(string namePart)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Players
                .Where(p => p.Name.Contains(namePart))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task AddPlayerAsync(Player player)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Players.Add(player);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePlayerAsync(Player updatedPlayer)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Players.Update(updatedPlayer);
            await context.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(int id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var player = await context.Players.FindAsync(id);
            if (player is not null)
            {
                context.Players.Remove(player);
                await context.SaveChangesAsync();
            }
        }
    }
}

