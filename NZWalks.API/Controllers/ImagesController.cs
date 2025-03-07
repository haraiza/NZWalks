﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidaFileUpload(request);

            if(ModelState.IsValid)
            {

            }
            
            return BadRequest(ModelState);
        }

        private void ValidaFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jped", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.FileName)))
                ModelState.AddModelError("file", "Unsupported file extension");

            //this number (10485760) is 10 MB
            if (request.File.Length > 10485760)
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            
        }
    }
}
