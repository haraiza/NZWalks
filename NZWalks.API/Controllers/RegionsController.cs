using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:xxxx/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public IRegionRepository regionRepository { get; }

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository) 
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }


        // GET ALL REGIONS
        // GET: https://localhost:xxxx/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from Database - Domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageURL = regionDomain.RegionImageURL
                });
            }

            // Return DTOs
            return Ok(regionsDto);
        }


        // GET REGION BY ID
        // GET: https://localhost:xxxx/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {

            // Get Data from Database - Domain models
            var regionDomain = await regionRepository.GetRegionByIdAsync(id);

            if (regionDomain is null)
                return NotFound();

            // Map Domain Models to DTOs
            var regionsDto = new RegionDto
            {

                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageURL = regionDomain.RegionImageURL

            };

            // Return DTOs
            return Ok(regionsDto);
        }



        // POST To Create New Region
        // POST: https://localhost:xxxx/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageURL = addRegionRequestDto.RegionImageURL
            };


            // Use Domain Modl to create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            

            // Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };

            return CreatedAtAction(nameof(GetRegionById), new {id = regionDomainModel.Id}, regionDto);
        }


        // Update Region
        // PUT: https://localhost:xxxx/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // MAP DTO to Domain Name
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageURL = updateRegionRequestDto.RegionImageURL
            };

            // Check if region exists
            await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
                return NotFound();

            // Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };

            return Ok(regionDto);
        }


        // Delete Region
        // DELETE: https://localhost:xxxx/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            // Return deleted Region back
            // MAP Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };

            return Ok(regionDto);
        }
    }
}
