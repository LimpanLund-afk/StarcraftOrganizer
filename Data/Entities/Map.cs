namespace StarcraftOrganizer.Data.Entities
{
    public class Map
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<ChallengeMaps> ChallengeMaps { get; set; } = new List<ChallengeMaps>();
    }
}
