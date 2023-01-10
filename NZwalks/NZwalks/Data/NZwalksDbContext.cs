using Microsoft.EntityFrameworkCore;
using NZwalks.Models.Domain;

namespace NZwalks.Data
{
    public class NZwalksDbContext: DbContext
    {
        public NZwalksDbContext(DbContextOptions<NZwalksDbContext> options): base(options)
        {

        }

        public DbSet<Region>    Regions { get; set; }
        public DbSet<Walk>    Walks { get; set; }
        public DbSet<WalkDifficulty>    walkDifficulties { get; set; }
    }
}
