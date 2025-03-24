using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            // Valida la imagen subida
            ValidaFileUpload(request);

            if(ModelState.IsValid)
            {
                // Convierte el DTO a un Domain Model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };

                // Envia la imagen al repositorio
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            
            return BadRequest(ModelState);
        }

        private void ValidaFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jped", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
                ModelState.AddModelError("file", "Unsupported file extension");

            // Este numero (10485760) es equivalente a 10 MB  
            if (request.File.Length > 10485760)
                ModelState.AddModelError("file", "El archivo pesa mas de 10 MB, por favor selecciona uno mas pequeño.");
            
        }
    }
}
