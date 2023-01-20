namespace NZwalks.Models.DTOs
{
    public class WalkRequestModel
    {
        public string? Name { get; set; }
        public double? Length { get; set; }
        public Guid WalkDifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
