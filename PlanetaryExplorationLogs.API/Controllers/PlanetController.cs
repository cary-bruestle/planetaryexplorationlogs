using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MissionaryExplorationLogs.API.Requests.Commands.Missions;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Requests.Commands.Discoveries;
using PlanetaryExplorationLogs.API.Requests.Commands.Planets;
using PlanetaryExplorationLogs.API.Requests.Queries.Missions;
using PlanetaryExplorationLogs.API.Requests.Queries.Planets;
using PlanetaryExplorationLogs.API.Utility.Patterns;

namespace PlanetaryExplorationLogs.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly PlanetExplorationDbContext _context;
        public PlanetController(PlanetExplorationDbContext context)
        {
            _context = context;
        }

        // GET: api/planet/dropdown
        [HttpGet("dropdown")]
        public async Task<ActionResult<RequestResult<List<PlanetDropdownDto>>>> GetPlanetsDropdownList()
        {
            var query = new GetPlanetsDropdownList_Query(_context);
            return await query.ExecuteAsync();
        }

        // GET: api/planet/list
        [HttpGet("list")]
        public async Task<ActionResult<RequestResult<List<Planet>>>> GetPlanets()
        {
            var query = new GetPlanets_Query(_context);
            return await query.ExecuteAsync();
        }

        // POST: api/planet
        [HttpPost]
        public async Task<ActionResult<RequestResult<int>>> CreatePlanet([FromBody] PlanetFormDto planet)
        {
            var cmd = new AddPlanet_Command(_context, planet);
            return await cmd.ExecuteAsync();
        }

        // PUT: api/planet
        [HttpPut]
        public async Task<ActionResult<RequestResult<int>>> UpdatePlanet([FromBody] Planet planet)
        {
            var cmd = new UpdatePlanet_Command(_context, planet);
            return await cmd.ExecuteAsync();
        }

        // DELETE: api/planet/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestResult<int>>> DeletePlanet(int id)
        {
            var cmd = new DeletePlanet_Command(_context, id);
            return await cmd.ExecuteAsync();
        }

        // Added for fun
        // GET: api/planet/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestResult<Planet>>> GetPlanet(int id)
        {
            // Retrieve a specific planet by ID.
            var query = new GetPlanet_Query(_context, id);
            return await query.ExecuteAsync();
        }




    }
}
