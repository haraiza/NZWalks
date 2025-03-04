using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Walk> Walk { get; set; }
        //public object Regions { get; internal set; }
    }
}
