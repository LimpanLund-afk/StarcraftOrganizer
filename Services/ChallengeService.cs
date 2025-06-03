using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Data.DataContext;
using StarcraftOrganizer.Data.Entities;

namespace StarcraftOrganizer.Services
{
    public class ChallengeService : BaseService
    {
        private readonly IDbContextFactory<DataContext> _contextFactory;

        public ChallengeService(IDbContextFactory<DataContext> factory, IDbContextFactory<DataContext> dbContextFactory) : base(factory)
        {
            _contextFactory = dbContextFactory;
        }

        public async Task<List<Challenge>> GetAllChallengesAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Challenges
                .Include(c => c.Player1)
                .Include(c => c.Player2)
                .Include(c => c.ChallengeMaps)
                    .ThenInclude(cm => cm.Map)
                .Include(c => c.Player1VetoMap)
                .Include(c => c.Player2VetoMap)
                .Include(c => c.Matches)
                .ToListAsync();
        }

        public async Task<Challenge?> GetByIdAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Challenges
                .Include(c => c.Player1)
                .Include(c => c.Player2)
                .Include(c => c.ChallengeMaps)
                    .ThenInclude(cm => cm.Map)
                .Include(c => c.Player1VetoMap)
                .Include(c => c.Player2VetoMap)
                .Include(c => c.Matches)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Challenge challenge)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Challenges.Add(challenge);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Challenge challenge)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Challenges.Update(challenge);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var challenge = await context.Challenges.FindAsync(id);
            if (challenge != null)
            {
                context.Challenges.Remove(challenge);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Challenge>> GetChallengesByPlayerIdAsync(int playerId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Challenges
                .Include(c => c.Player1)
                .Include(c => c.Player2)
                .Where(c => c.Player1Id == playerId || c.Player2Id == playerId)
                .ToListAsync();
        }

        public async Task<List<Challenge>> GetActiveChallengesAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Challenges
                .Where(c => c.Status == ChallengeStatus.PendingVeto
                         || c.Status == ChallengeStatus.ReadyToPlay
                         || c.Status == ChallengeStatus.InProgress)
                .Include(c => c.Player1)
                .Include(c => c.Player2)
                .ToListAsync();
        }

        public async Task SetVetoMapAsync(int challengeId, int playerNumber, int mapId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var challenge = await context.Challenges.FindAsync(challengeId);
            if (challenge == null) return;

            if (playerNumber == 1)
                challenge.Player1VetoMapId = mapId;
            else if (playerNumber == 2)
                challenge.Player2VetoMapId = mapId;

            await context.SaveChangesAsync();
        }

        public async Task<bool> AreBothVetosSetAsync(int challengeId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var challenge = await context.Challenges.FindAsync(challengeId);
            return challenge?.Player1VetoMapId != null && challenge?.Player2VetoMapId != null;
        }
    }



}
