using Microsoft.EntityFrameworkCore;
using NZwalks.Data;
using NZwalks.Models.Domain;
using NZwalks.Models.DTOs;

namespace NZwalks.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZwalksDbContext _nZwalksDbContext;

        public WalkRepository(NZwalksDbContext nZwalksDbContext)
        {
            _nZwalksDbContext = nZwalksDbContext;
        }

        public async Task<bool> AddWalk(Walk walk)
        {
            if (walk != null)
            {
                await this._nZwalksDbContext.Walks.AddAsync(walk);
                await this._nZwalksDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteWalk(Guid id)
        {
            var walk = await _nZwalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk != null)
            {
                _nZwalksDbContext.Walks.Remove(walk);
                await _nZwalksDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await this._nZwalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkById(Guid id)
        {
            var response = new Walk();
            response = null;
            response = await _nZwalksDbContext.Walks
                           .Include(x => x.Region)
                           .Include(x => x.WalkDifficulty)
                           .FirstOrDefaultAsync(x => x.Id == id);

            return response;
        }

        public async Task<bool> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await _nZwalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk != null)
            {
                existingWalk.Name = walk.Name;
                existingWalk.Length = walk.Length;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;

                _nZwalksDbContext.Walks.Update(existingWalk);
                await _nZwalksDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
