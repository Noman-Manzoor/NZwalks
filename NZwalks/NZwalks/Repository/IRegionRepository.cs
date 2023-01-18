using NZwalks.Models.Domain;

namespace NZwalks.Repository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
        Task<Region> GetRegionById(Guid id);
        Task<bool> AddRegion(Region region);
        Task<bool> DeleteRegion(Guid id);
        Task<bool> UpdateRegionAsync(Guid id, Region region);
    }
}
