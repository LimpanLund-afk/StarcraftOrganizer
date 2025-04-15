using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using StarcraftOrganizer.Data.DataContext;
using StarcraftOrganizer.Data.Entities;

namespace StarcraftOrganizer.Services
{
    public class MapService :BaseService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public MapService(IDbContextFactory<DataContext> factory, IDbContextFactory<DataContext> dbContextFactory) : base(factory)
        {
            _contextFactory = dbContextFactory;
        }

        public async Task<List<Map>> GetAllMapssAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Maps.ToListAsync();
        }

        public async Task<Map?> GetMapByIdAsync(int Id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Maps
                .FirstOrDefaultAsync(m => m.Id == Id);
        }

        public async Task<List<Map>> SearchMapsByNameAsync(string namePart)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Maps
                .Where(p => p.Name.Contains(namePart))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task AddMapAsync(Map Map)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Maps.Add(Map);
            await context.SaveChangesAsync();
        }

        public async Task UpdateMapAsync(Map updatedMap)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Maps.Update(updatedMap);
            await context.SaveChangesAsync();
        }

        public async Task DeleteMapAsync(int id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var Map = await context.Maps.FindAsync(id);
            if (Map is not null)
            {
                context.Maps.Remove(Map);
                await context.SaveChangesAsync();
            }
        }
    }
}
