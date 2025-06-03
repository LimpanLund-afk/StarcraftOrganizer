using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Data.DataContext;
using StarcraftOrganizer.Data.Entities;


namespace StarcraftOrganizer.Services
{
    public class MatchService : BaseService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public MatchService(IDbContextFactory<DataContext> factory, IDbContextFactory<DataContext> dbContextFactory) : base(factory)
        {
            _contextFactory = dbContextFactory;
        }

        public async Task<List<Match>> GetAllMatchessAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Matches.ToListAsync();
        }

        public async Task<List<Match>> GetMatchesByChallengeIdAsync(int challengeId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Matches
                .Where(m => m.ChallengeId == challengeId)
                .ToListAsync();
        }

        public async Task<List<Match>> GetMatchesByPlayerIdAsync(int playerId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Matches
                .Where(m => m.Player1Id == playerId || m.Player2Id == playerId)
                .ToListAsync();
        }
    }
}
