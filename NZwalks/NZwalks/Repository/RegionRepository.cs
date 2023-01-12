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

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await this._nZwalksDbContext.Regions.ToListAsync();
        }
    }
}
