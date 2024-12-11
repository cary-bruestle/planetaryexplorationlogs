using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;
using PlanetaryExplorationLogs.API.Data.Models;
namespace PlanetaryExplorationLogs.API.Requests.Queries.Discoveries
{

    public class GetDiscovery_Query : RequestBase<Discovery>
    {
        private readonly int _discoveryId;

        public GetDiscovery_Query(PlanetExplorationDbContext context, int discoveryId)
            : base(context)
        {
            _discoveryId = discoveryId;
        }

        // The authorizer, validator, and handler run in that order. If any fail, the query will not be executed.

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer => new GetDiscovery_Authorizer(DbContext);

        // The validator is optional and can be removed if not needed
        public override IValidator Validator => new GetDiscovery_Validator(DbContext, _discoveryId);

        // The handler is mandatory to have for every query
        public override IHandler<Discovery> Handler => new GetDiscovery_Handler(DbContext, _discoveryId);
    }

    // The handler class is responsible for executing the query
    public class GetDiscovery_Handler : HandlerBase<Discovery>
    {
        private readonly int _discoveryId;

        public GetDiscovery_Handler(PlanetExplorationDbContext context, int discoveryId)
            : base(context)
        {
            _discoveryId = discoveryId;
        }

        public override async Task<RequestResult<Discovery>> HandleAsync()
        {
            // Write your query here
            var someDiscovery = await DbContext.Discoveries.Where(d => d.Id.Equals(_discoveryId)).FirstAsync();

            // Return the data
            var result = new RequestResult<Discovery> { Data = someDiscovery };

            return result;
        }
    }

    // The authorizer class is responsible for any additional authorization logic
    public class GetDiscovery_Authorizer : AuthorizerBase
    {
        public GetDiscovery_Authorizer(PlanetExplorationDbContext context)
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
    public class GetDiscovery_Validator : ValidatorBase
    {
        private readonly int _discoveryId;

        public GetDiscovery_Validator(PlanetExplorationDbContext context, int id)
            : base(context)
        {
            _discoveryId = id;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            await Task.CompletedTask;

            if (!await DbContext.Discoveries.AnyAsync(d => d.Id == _discoveryId))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "No discovery exists with the given ID.");
            }

            return await ValidResultAsync();
        }
    }

}
