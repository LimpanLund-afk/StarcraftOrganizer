using System.ComponentModel.DataAnnotations.Schema;

namespace StarcraftOrganizer.Data.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        // Referenser till de två spelarna
        public int Player1Id { get; set; }
        public Player Player1 { get; set; }

        public int Player2Id { get; set; }
        public Player Player2 { get; set; }

        // Vilken karta som spelades
        public int MapId { get; set; }
        public Map Map { get; set; }

        // True om Player1 vann, false om Player2 vann.
        public bool Player1Won { get; set; }
       
        [NotMapped]
        public Player Winner => Player1Won ? Player1 : Player2;
    }
}
