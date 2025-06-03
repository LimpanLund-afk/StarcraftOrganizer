using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using StarcraftOrganizer.Data.DataContext;
using System.Security.Claims;

namespace StarcraftOrganizer.Infra
{
    public class CoreAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CoreAuthenticationStateProvider(ProtectedLocalStorage localStorage, IDbContextFactory<DataContext> dbContextFactory)
        {
            _localStorage = localStorage;
            _dbContextFactory = dbContextFactory;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var result = await _localStorage.GetAsync<UserSession>("UserSession");
                var userSession = result.Success ? result.Value : null;

                if (userSession != null && await ValidateUserSession(userSession))
                {
                    var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Name),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }, "CustomAuth"));

                    return new AuthenticationState(claimsPrincipal);
                }
                else
                {
                    if (userSession != null)
                    {
                        await _localStorage.DeleteAsync("UserSession");
                    }

                    return new AuthenticationState(_anonymous);
                }
            }
            catch
            {
                return new AuthenticationState(_anonymous);
            }
        }

        public async Task<bool> ValidateUserSession(UserSession session)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var player = await context.Players.FirstOrDefaultAsync(p => p.Id == session.UserId);

            if (player != null && session.Token == UserSession.GetTokenFromUser(player))
            {
                return true;
            }

            return false;
        }

        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await _localStorage.SetAsync("UserSession", userSession);

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.Name),
                new Claim(ClaimTypes.Role, userSession.Role)
            }, "CustomAuth"));
            }
            else
            {
                await _localStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
