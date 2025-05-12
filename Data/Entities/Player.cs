using Microsoft.AspNetCore.Identity;

namespace StarcraftOrganizer.Data.Entities
{
    public class Player : IdentityUser
    {

        public string Name { get; set; }
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
