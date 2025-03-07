using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        { }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Walk> Walk { get; set; }

        public DbSet<Image> Image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties
            // Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("f6f671a5-c591-4737-97b4-e4c18b17019d"),
                    Name = "Easy"

                },
                new Difficulty()
                {
                    Id = Guid.Parse("52ae6919-ed9c-4f59-b684-e6dd966411fc"),
                    Name = "Medium"

                },
                new Difficulty()
                {
                    Id = Guid.Parse("574f113d-d1b2-48aa-a7b0-c4b083e0cc9f"),
                    Name = "Hard"
                }
            };

            // Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            // Seed data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("5cdf0c65-5b71-481f-a1ee-d6d2c3c7a842"),
                    Code = "AKL",
                    Name = "Auckland",
                    RegionImageURL = null
                },
                new Region
                {
                    Id = Guid.Parse("7a18137d-388f-4da7-a122-ba610f9d29fc"),
                    Code = "NTL",
                    Name = "Northland",
                    RegionImageURL = null
                },
                new Region
                {
                    Id = Guid.Parse("5de6ad77-2e24-4902-b921-12f7daed208a"),
                    Code = "BOP",
                    Name = "Bay Of Plenty",
                    RegionImageURL = null
                },
                new Region
                {
                    Id = Guid.Parse("83325d54-42d5-48d2-8867-63d457718080"),
                    Code = "WGN",
                    Name = "Wellington",
                    RegionImageURL = null
                },

                new Region
                {
                    Id = Guid.Parse("813765d6-56e9-495a-b3df-636f81f11241"),
                    Code = "NSN",
                    Name = "Nelson",
                    RegionImageURL = null
                },
                  new Region
                {
                    Id = Guid.Parse("8bc47a6a-a25b-4ad1-a7fb-9f7caa02a321"),
                    Code = "STL",
                    Name = "Southland",
                    RegionImageURL = null
                },
            };

            // Seed regions to the database
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
