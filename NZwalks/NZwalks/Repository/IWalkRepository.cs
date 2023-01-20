using NZwalks.Models.Domain;

namespace NZwalks.Repository
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> GetWalkById(Guid id);
        Task<bool> AddWalk(Walk walk);
        Task<bool> DeleteWalk(Guid id);
        Task<bool> UpdateWalkAsync(Guid id, Walk walk);
    }
}
