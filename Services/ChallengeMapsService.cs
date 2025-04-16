using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Data.DataContext;
using StarcraftOrganizer.Data.Entities;

namespace StarcraftOrganizer.Services
{
    public class ChallengeMapsService : BaseService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public ChallengeMapsService(IDbContextFactory<DataContext> factory, IDbContextFactory<DataContext> dbContextFactory) : base(factory)
        {
            _contextFactory = dbContextFactory;
        }


        public async Task AddAsync(IEnumerable<ChallengeMaps> challengeMaps)
        {
            if (challengeMaps == null || !challengeMaps.Any())
                return;
            using var context = await _contextFactory.CreateDbContextAsync();

            context.ChallengeMaps.AddRange(challengeMaps);
            await context.SaveChangesAsync();
        }
    }
}
