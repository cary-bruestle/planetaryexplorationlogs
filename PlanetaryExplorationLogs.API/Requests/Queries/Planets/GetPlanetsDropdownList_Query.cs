using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Planets
{
    public class GetPlanetsDropdownList_Query : RequestBase<List<PlanetDropdownDto>>
    {
        public GetPlanetsDropdownList_Query(PlanetExplorationDbContext context)
            : base(context)
        {
        }

        public override IValidator Validator => new GetPlanetsDropdownList_Validator(DbContext);
        public override IHandler<List<PlanetDropdownDto>> Handler => new GetPlanetsDropdownList_Handler(DbContext);
    }

    public class GetPlanetsDropdownList_Validator : ValidatorBase
    {
        public GetPlanetsDropdownList_Validator(PlanetExplorationDbContext context)
            : base(context)
        {
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            if (!DbContext.Planets.Any())
            {
                return await InvalidResultAsync(
                    HttpStatusCode.NotFound,
                    "There are no planet records. Please add a planet.");
            }

            return await ValidResultAsync();
        }
    }

    public class GetPlanetsDropdownList_Handler : HandlerBase<List<PlanetDropdownDto>>
    {

        public GetPlanetsDropdownList_Handler(PlanetExplorationDbContext context)
            : base(context)
        {
        }

        public override async Task<RequestResult<List<PlanetDropdownDto>>> HandleAsync()
        {
            var planets = await DbContext.Planets
                .OrderBy(p => p.Name)
                .Select(p => new PlanetDropdownDto
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();

            var result = new RequestResult<List<PlanetDropdownDto>> { Data = planets };

            return result;
        }
    }
}
