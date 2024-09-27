using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domian;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficuties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image>Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var dificulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id =Guid.Parse("e37a8d11-2538-4176-84c6-8def70893507"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id =Guid.Parse("a5fcf75b-64c9-40c5-8d40-f8efe046cac9"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id =Guid.Parse("ecfd3dee-79b4-4d49-b60f-1ad2f245a159"),
                    Name = "Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(dificulties);

            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("929a8a0e-580d-4aa7-abf2-d888b683e0c1"),
                    Code = "tki",
                    Name = "kielce",
                    RegionImageUrl = "kielce.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("77f38b06-d675-4273-893b-98e4faf018b9"),
                    Code = "tkn",
                    Name = "konskie",
                    RegionImageUrl = "konskie.jpg"
                },
                new Region()
                {
                    Id = Guid.Parse("82bfdda6-aaeb-4367-b009-90c8ef7332ff"),
                    Code = "so",
                    Name = "sosnowiec",
                    RegionImageUrl = "sosonowiec"
                },
                new Region()
                {
                    Id = Guid.Parse("298011b4-b9fe-4fc7-9722-4033f4f9a320"),
                    Code = "krk",
                    Name = "krakow",
                    RegionImageUrl = "krakow"
                },
                new Region()
                {
                    Id = Guid.Parse("3715645b-f994-4174-ad6f-9ba189b412c7"),
                    Code = "po",
                    Name = "poznan",
                    RegionImageUrl = "poznan"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }

        

    }
}
