using NZwalks.Models.Domain;

namespace NZwalks.Models.DTOs
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double? Length { get; set; }
        public Guid WalkDifficultyId { get; set; }
        public Guid RegionId { get; set; }


        // Navigation Property
        public RegionDTO? Region { get; set; }
        public WalkDifficultyDTO? WalkDifficulty { get; set; }
    }
}
