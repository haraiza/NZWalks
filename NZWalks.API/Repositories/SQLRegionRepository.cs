using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Region.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Region.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
                return null;

            dbContext.Region.Remove(existingRegion);
            await dbContext.SaveChangesAsync(); 
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Region.ToListAsync();
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            return await dbContext.Region.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Region.FirstOrDefaultAsync(x=>x.Id == id);
            if(existingRegion == null) 
                return null;

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageURL = region.RegionImageURL;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
