using System.ComponentModel.DataAnnotations.Schema;

namespace StarcraftOrganizer.Data.Entities
{
    public class Challenge
    {
        public int Id { get; set; }


        public int Player1Id { get; set; }
        public Player Player1 { get; set; }

        public int Player2Id { get; set; }
        public Player Player2 { get; set; }

        public BestOfFormat SeriesFormat { get; set; } 


        public ChallengeStatus Status { get; set; } 


        public DateTime CreationDate { get; set; } = DateTime.UtcNow;


        public int? Player1VetoMapId { get; set; }
        [ForeignKey("Player1VetoMapId")] 
        public Map? Player1VetoMap { get; set; }

        public int? Player2VetoMapId { get; set; }
        [ForeignKey("Player2VetoMapId")] 
        public Map? Player2VetoMap { get; set; }

        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();


         public virtual ICollection<Map> MapPoolSnapshot { get; set; } = new List<Map>();
    }

    public enum BestOfFormat
    {
        BestOf1 = 1,
        BestOf3 = 3,
        BestOf5 = 5,
        BestOf7 = 7,
        BestOf10 = 10
    }

    public enum ChallengeStatus
    {
        PendingVeto,    
        ReadyToPlay,    
        InProgress,     
        Completed,      
        Cancelled       
    }
}
