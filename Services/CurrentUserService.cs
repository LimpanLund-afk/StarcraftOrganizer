using global::StarcraftOrganizer.Data.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using StarcraftOrganizer.Data.Entities;




namespace StarcraftOrganizer.Services
{    
    public class CurrentUserService
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly UserManager<Player> _userManager;

        public CurrentUserService(AuthenticationStateProvider authStateProvider, UserManager<Player> userManager)
        {
            _authStateProvider = authStateProvider;
            _userManager = userManager;
        }

        public async Task<Player?> GetCurrentUserAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not { IsAuthenticated: true })
                return null;

            return await _userManager.GetUserAsync(user);
        }
    }
    
}
