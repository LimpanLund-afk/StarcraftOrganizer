using static StarcraftOrganizer.Data.Entities.Player;

namespace StarcraftOrganizer.Data.Entities
{
    public class Player
    {
        public int Id { get; set; }
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
