using Microsoft.EntityFrameworkCore;
using NZwalks.Data;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;

namespace NZwalks.Repository
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZwalksDbContext _nZwalksDbContext;

        public WalkDifficultyRepository(NZwalksDbContext nZwalksDbContext)
        {
            this._nZwalksDbContext = nZwalksDbContext;
        }

        public async Task<bool> AddWalkDifficulty(WalkDifficulty newWalkDifficulty)
        {
           if(newWalkDifficulty != null)
            {
                await _nZwalksDbContext.walkDifficulties.AddAsync(newWalkDifficulty);
                await _nZwalksDbContext.SaveChangesAsync();
                return true;
            }
            return false;
            
        }

        public async Task<bool> DeleteWalkDifficulty(Guid id)
        {
            //Question 1

            //var walkDifficulty = _nZwalksDbContext.walkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            //if (walkDifficulty != null)
            //{
            //    _nZwalksDbContext.walkDifficulties.Remove(walkDifficulty);
            //    await _nZwalksDbContext.SaveChangesAsync();
            //    return true;
            //}
            //return false;

            var walkDifficulty = await _nZwalksDbContext.walkDifficulties.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (walkDifficulty != null)
            {
                _nZwalksDbContext.walkDifficulties.Remove(walkDifficulty);
                await _nZwalksDbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties()
        {
            return await _nZwalksDbContext.walkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficultyById(Guid id)
        {
            return await _nZwalksDbContext.walkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateWalkDifficulty(Guid id, WalkDifficulty updatedwalkDifficulty)
        {
            // Question 2 
            var walkDifficulty = await _nZwalksDbContext.walkDifficulties.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (walkDifficulty != null)
            {
                walkDifficulty.Code = updatedwalkDifficulty.Code;   

                _nZwalksDbContext.walkDifficulties.Update(walkDifficulty);
                await _nZwalksDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
