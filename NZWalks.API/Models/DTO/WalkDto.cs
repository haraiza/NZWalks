﻿namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
