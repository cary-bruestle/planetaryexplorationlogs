using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Requests.Commands.Planets;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Planets
{
    public class GetPlanets_Query : RequestBase<List<Planet>>
    {
        public GetPlanets_Query(PlanetExplorationDbContext context)
        : base(context)
        {
        }

        public override IHandler<List<Planet>> Handler => new GetPlanets_Handler(DbContext);
    }

    public class GetPlanets_Handler : HandlerBase<List<Planet>>
    {
        public GetPlanets_Handler(PlanetExplorationDbContext context)
            : base(context)
        {
        }

        public override async Task<RequestResult<List<Planet>>> HandleAsync()
        {
            var planets = await DbContext.Planets
                .OrderBy(t => t.Name)
                .ToListAsync();

            var result = new RequestResult<List<Planet>> { Data = planets };

            return result;
        }
    }
}
