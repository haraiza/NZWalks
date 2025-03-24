using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    // https://localhost:xxxx/api/regions
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger) 
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        // GET ALL REGIONS
        // GET: https://localhost:xxxx/api/regions
        [HttpGet]
        [Authorize(Roles ="Reader")] // Solo usuarios con el Rol 'Reader' pueden ejecutar este metodo
        public async Task<IActionResult> GetAll()
        {
            try
            {
                logger.LogInformation("El metodo GetAllRegions fue invocado");

                //logger.LogWarning("This is a warning log");
                //logger.LogError("This is a error log");

                // Obtiene los datos de la base de datos - Domain models
                var regionsDomain = await regionRepository.GetAllAsync();
                logger.LogInformation($"Termino GetAllRegions con los datos: {JsonSerializer.Serialize(regionsDomain)}");

                // Convierte el Region Domain a una lista de RegionDTOs
                return Ok(mapper.Map<List<Region>>(regionsDomain));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
            
            
        }


        // GET REGION BY ID
        // GET: https://localhost:xxxx/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetRegionByIdAsync(id);

            if (regionDomain is null)
                return NotFound();

            // Convierte a RegionDTO
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }


        // POST para crear una nueva region
        // POST: https://localhost:xxxx/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Convierte el RequestDto a Region Domain
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Envia al repositorio el regionDomain y regresa el nuevo regionDomain
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetRegionById), new {id = regionDomainModel.Id}, regionDto);
        }


        // Update Region
        // PUT: https://localhost:xxxx/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            // Revisa si la region existe
            await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
                return NotFound();

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }


        // Delete Region
        // DELETE: https://localhost:xxxx/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
                return NotFound();

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
