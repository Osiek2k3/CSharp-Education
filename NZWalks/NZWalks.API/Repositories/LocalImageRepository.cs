using NZWalks.API.Data;
using NZWalks.API.Models.Domian;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            NZWalksDbContext nZWalksDbContext) {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = nZWalksDbContext;
        }
        public  async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Image/{image.FileName}{image.FileExtension}";
            
            image.FilePath = urlFilePath;

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
