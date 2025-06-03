using Microsoft.AspNetCore.Identity;

namespace StarcraftOrganizer.Data.Entities
{
    public class Player
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public string? Salt { get; set; }

        public string UserName { get; set; }

        public FavouredRace Race { get; set; }


        public virtual ICollection<Challenge>? ChallengesAsPlayer1 { get; set; } = new List<Challenge>();
        public virtual ICollection<Challenge>? ChallengesAsPlayer2 { get; set; } = new List<Challenge>();
    }

    public enum FavouredRace
    {
        Zerg,     
        Protoss,  
        Terran   
    }
}
