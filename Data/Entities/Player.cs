using static StarcraftOrganizer.Data.Entities.Player;

namespace StarcraftOrganizer.Data.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FavouredRace Race { get; set; }
        
    }

public enum FavouredRace
{
    Zerg,     
    Protoss,  
    Terran   
}
}
