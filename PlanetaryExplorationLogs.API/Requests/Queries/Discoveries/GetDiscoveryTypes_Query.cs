using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Discoveries
{
    public class GetDiscoveryTypes_Query : RequestBase<List<DiscoveryType>>
    {
        public GetDiscoveryTypes_Query(PlanetExplorationDbContext context)
            : base(context)
        {
        }

        public override IHandler<List<DiscoveryType>> Handler => new GetDiscoveryTypes_Handler(DbContext);
    }

    public class GetDiscoveryTypes_Handler : HandlerBase<List<DiscoveryType>>
    {
        public GetDiscoveryTypes_Handler(PlanetExplorationDbContext context)
            : base(context)
        {
        }

        public override async Task<RequestResult<List<DiscoveryType>>> HandleAsync()
        {
            var discoveryTypes = await DbContext.DiscoveryTypes
                .OrderBy(t => t.Name)
                .ToListAsync();

            var result = new RequestResult<List<DiscoveryType>>
            {
                Data = discoveryTypes
            };

            return result;
        }
    }
}
