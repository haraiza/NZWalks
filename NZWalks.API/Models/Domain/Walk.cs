namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }


        // Navigation properties
        public Difficulty Difficulty { get; set; } // Referencia cruzada con la tabla Difficulty
        public Region Region { get; set; } // Referencia cruzada con la tabla Region
    }
}
