using NZwalks.Models.Domain;

namespace NZwalks.Repository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
