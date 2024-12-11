using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Missions
{
    public class GetMissions_Query : RequestBase<List<Mission>>
	{
		public GetMissions_Query(PlanetExplorationDbContext context)
			: base(context)
		{
		}

		public override IHandler<List<Mission>> Handler => new GetMissions_Handler(DbContext);
	}

    public class GetMissions_Handler : HandlerBase<List<Mission>>
    {
        public GetMissions_Handler(PlanetExplorationDbContext context)
            : base(context)
        {
        }

        public override async Task<RequestResult<List<Mission>>> HandleAsync()
        {
            var missions = await DbContext.Missions
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            var result = new RequestResult<List<Mission>> { Data = missions };

            return result;
        }
    }
}
