using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnviroment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnviroment, IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
        {
            this.webHostEnviroment = webHostEnviroment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public async Task<Image> Upload(Image image)
        {
            // Obtiene cual sera el path de la imagen
            var localFilePath = Path.Combine(webHostEnviroment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            // Copia la imagen a path
            await image.File.CopyToAsync(stream);

            // https://localhost:7250/images/image.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            // Agrega la imagen a la tabla Images 
            await dbContext.Image.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
