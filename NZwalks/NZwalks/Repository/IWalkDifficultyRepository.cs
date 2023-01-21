using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;

namespace NZwalks.Repository
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties();
        Task<WalkDifficulty> GetWalkDifficultyById(Guid id);
        Task<bool> AddWalkDifficulty(WalkDifficulty newWalkDifficulty);
        Task<bool> UpdateWalkDifficulty(Guid id, WalkDifficulty updatedWalkDifficulty);
        Task<bool> DeleteWalkDifficulty(Guid id);
    }
}
