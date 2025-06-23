using System.ComponentModel.DataAnnotations.Schema;

namespace StarcraftOrganizer.Data.Entities
{
    public class Match // (Befintlig klass, utökad)
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Player1Id { get; set; }
        public Player Player1 { get; set; }
        public int Player2Id { get; set; }
        public Player Player2 { get; set; }
        public int MapId { get; set; }
        public Map Map { get; set; }
        public bool? Player1Won { get; set; }

        public ChosenRace Player1Race { get; set; } // NYTT: Vald ras för matchen
        public ChosenRace Player2Race { get; set; }

        public enum ChosenRace
        {
            Zerg,
            Protoss,
            Terran,
            Random 
        }

        // NYTT: Länk till vilken Challenge denna match tillhör
        public int ChallengeId { get; set; }
        public virtual Challenge Challenge { get; set; }

        [NotMapped]
        public Player Winner => Player1Won.Value ? Player1 : Player2;
    }
}
