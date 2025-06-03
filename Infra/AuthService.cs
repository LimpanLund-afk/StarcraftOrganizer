using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Data.DataContext;
using StarcraftOrganizer.Data.Entities;
using System.Security.Claims;

namespace StarcraftOrganizer.Infra
{
    public class AuthService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly CoreAuthenticationStateProvider _authStateProvider;

        public AuthService(IDbContextFactory<DataContext> dbContextFactory, AuthenticationStateProvider authStateProvider)
        {
            _dbContextFactory = dbContextFactory;
            _authStateProvider = (CoreAuthenticationStateProvider)authStateProvider;
        }

        public async Task<bool> Login(string userName, string password)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var player = await context.Players
                .FirstOrDefaultAsync(p => p.UserName == userName);

            if (player != null && player.Password == Hash.GetHash(password + player.Salt, Hash.HashType.SHA256))
            {
                var session = new UserSession
                {
                    UserId = player.Id,
                    Name = player.UserName,
                    Role = "Player", // Om du vill ha rollhantering
                    Token = UserSession.GetTokenFromUser(player) // Valfri, kan vara dummy-token
                };

                await _authStateProvider.UpdateAuthenticationState(session);
                return true;
            }

            await _authStateProvider.UpdateAuthenticationState(null);
            return false;
        }

        public async Task Logout()
        {
            await _authStateProvider.UpdateAuthenticationState(null);
        }

        public async Task<Player?> GetCurrentPlayer()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var name = user.FindFirst(ClaimTypes.Name)?.Value;
                if (string.IsNullOrWhiteSpace(name)) return null;

                using var context = await _dbContextFactory.CreateDbContextAsync();
                return await context.Players.FirstOrDefaultAsync(p => p.UserName == name);
            }

            return null;
        }

        public async Task<int?> GetLoggedInPlayerId()
        {
            var player = await GetCurrentPlayer();
            return player?.Id;
        }
    }
}
