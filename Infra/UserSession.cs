using StarcraftOrganizer.Data.Entities;

namespace StarcraftOrganizer.Infra
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = "Player";
        public string Token { get; set; } = string.Empty;

        public static string GetTokenFromUser(Player player)
        {
            
            return Hash.GetHash($"{player.Id}-{player.UserName}-{player.Salt}", Hash.HashType.SHA256);
        }
    }
}
