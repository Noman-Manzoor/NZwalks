using Microsoft.EntityFrameworkCore;
using NZwalks.Data;
using NZwalks.Models.Domain;

namespace NZwalks.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZwalksDbContext _nZwalksDbContext;
        public RegionRepository(NZwalksDbContext nZwalksDbContext)
        {
            this._nZwalksDbContext = nZwalksDbContext;
        }

        public async Task<bool> AddRegion(Region region)
        {
            if(region != null) {
                await this._nZwalksDbContext.Regions.AddAsync(region);
                await this._nZwalksDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRegion(Guid id)
        {
            var region = await _nZwalksDbContext.Regions.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(region != null)
            {
                _nZwalksDbContext.Regions.Remove(region);
                await _nZwalksDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await this._nZwalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionById(Guid id)
        {
            var response = await _nZwalksDbContext.Regions.Where(x => x.Id == id).FirstOrDefaultAsync(); 
            return response;
        }

        public async Task<bool> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await _nZwalksDbContext.Regions.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (existingRegion != null)
            {
                existingRegion.Code = region.Code;
                existingRegion.Name = region.Name;
                existingRegion.Lat = region.Lat;
                existingRegion.Long = region.Long;
                existingRegion.Area = region.Area;
                existingRegion.Population = region.Population;

                _nZwalksDbContext.Regions.Update(existingRegion);
                await _nZwalksDbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
