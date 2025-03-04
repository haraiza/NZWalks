using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    // https://localhost:xxxx/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }


        // GET ALL REGIONS
        // GET: https://localhost:xxxx/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data from Database - Domain models
            var regionsDomain = dbContext.Region.ToList();

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
        public IActionResult GetRegionById([FromRoute] Guid id)
        {

            // Get Data from Database - Domain models
            var regionDomain = dbContext.Region.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageURL = addRegionRequestDto.RegionImageURL
            };


            // Use Domain Modl to create Region
            dbContext.Region.Add(regionDomainModel);
            dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if region exists
            var regionDomainModel = dbContext.Region.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
                return NotFound();

            // Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageURL = updateRegionRequestDto.RegionImageURL;
            dbContext.SaveChanges();


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

    }
}
