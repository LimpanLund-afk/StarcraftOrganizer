namespace StarcraftOrganizer.Data.Entities
{
    public class ChallengeMap
    {
        public int ChallengeId { get; set; }
        public Challenge Challenge { get; set; }

        public int MapId { get; set; }
        public Map Map { get; set; }
    }
}
