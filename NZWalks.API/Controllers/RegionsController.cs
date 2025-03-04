using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

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
            var regions = dbContext.Region.ToList();

            return Ok(regions);
        }


        // GET REGION BY ID
        // GET: https://localhost:xxxx/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            //var region = dbContext.Region.Find(id);
            var region = dbContext.Region.FirstOrDefault(x => x.Id == id);

            if (region is null)
                return NotFound();

            return Ok(region);
        }
    }
}
