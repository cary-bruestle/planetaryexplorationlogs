using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;
using PlanetaryExplorationLogs.API.Data.Models;
namespace PlanetaryExplorationLogs.API.Requests.Queries.Planets
{

    public class GetPlanet_Query : RequestBase<Planet>
    {
        private readonly int _planetId;

        public GetPlanet_Query(PlanetExplorationDbContext context, int planetId)
            : base(context)
        {
            _planetId = planetId;
        }

        // The authorizer, validator, and handler run in that order. If any fail, the query will not be executed.

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer => new GetPlanet_Authorizer(DbContext);

        // The validator is optional and can be removed if not needed
        public override IValidator Validator => new GetPlanet_Validator(DbContext, _planetId);

        // The handler is mandatory to have for every query
        public override IHandler<Planet> Handler => new GetPlanet_Handler(DbContext, _planetId);
    }

    // The handler class is responsible for executing the query
    public class GetPlanet_Handler : HandlerBase<Planet>
    {
        private readonly int _planetId;

        public GetPlanet_Handler(PlanetExplorationDbContext context, int planetId)
            : base(context)
        {
            _planetId = planetId;
        }

        public override async Task<RequestResult<Planet>> HandleAsync()
        {
            // Write your query here
            var somePlanet = await DbContext.Planets.Where(d => d.Id.Equals(_planetId)).FirstAsync();

            // Return the data
            var result = new RequestResult<Planet> { Data = somePlanet };

            return result;
        }
    }

    // The authorizer class is responsible for any additional authorization logic
    public class GetPlanet_Authorizer : AuthorizerBase
    {
        public GetPlanet_Authorizer(PlanetExplorationDbContext context)
            : base(context)
        {
        }

        public override async Task<RequestResult> AuthorizeAsync()
        {
            // Obviously, this is dummy authorization logic. Replace it with your own.
            // Return AuthorizedResultAsync() if the operation is authorized, UnauthorizedResultAsync() otherwise.
            return await AuthorizedResultAsync();
        }
    }

    // The validator class is responsible for validating things before the query is executed
    public class GetPlanet_Validator : ValidatorBase
    {
        private readonly int _planetId;

        public GetPlanet_Validator(PlanetExplorationDbContext context, int planetid)
            : base(context)
        {
            _planetId = planetid;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            await Task.CompletedTask;

            if (!await DbContext.Planets.AnyAsync(d => d.Id == _planetId))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "No planet exists with the given ID.");
            }

            return await ValidResultAsync();
        }
    }

}
