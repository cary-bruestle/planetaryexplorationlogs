using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Discoveries
{
    public class DeleteDiscovery_Command : RequestBase<int>
    {
        private readonly int _discoveryId;

        public DeleteDiscovery_Command(PlanetExplorationDbContext context, int discoveryId)
            : base(context)
        {
            _discoveryId = discoveryId;
        }

        // The authorizer, validator, and handler run in that order. If any fail, the query will not be executed.

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer => new DeleteDiscovery_Authorizer(DbContext);

        // The validator is optional and can be removed if not needed
        public override IValidator Validator =>
            new DeleteDiscovery_Validator(DbContext, _discoveryId);

        // The handler is mandatory to have for every command
        public override IHandler<int> Handler =>
            new DeleteDiscovery_Handler(DbContext, _discoveryId);
    }

    // The authorizer class is responsible for any additional authorization logic
    public class DeleteDiscovery_Authorizer : AuthorizerBase
    {
        public DeleteDiscovery_Authorizer(PlanetExplorationDbContext context)
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
    public class DeleteDiscovery_Validator : ValidatorBase
    {
        private readonly int _discoveryId;

        public DeleteDiscovery_Validator(PlanetExplorationDbContext context, int discoveryId)
            : base(context)
        {
            _discoveryId = discoveryId;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            await Task.CompletedTask;

            if (!await DbContext.Discoveries.AnyAsync(m => m.Id == _discoveryId))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "No discovery exists with the given ID.");
            }

            return await ValidResultAsync();
        }
    }

    // The handler class is responsible for executing the query
    public class DeleteDiscovery_Handler : HandlerBase<int>
    {
        private readonly int _discoveryId;

        public DeleteDiscovery_Handler(PlanetExplorationDbContext context, int id)
            : base(context)
        {
            _discoveryId = id;
        }

        public override async Task<RequestResult<int>> HandleAsync()
        {
            var mission = await DbContext.Missions.FindAsync(_discoveryId);
            if (mission != null)
            {
                DbContext.Missions.Remove(mission);
                await DbContext.SaveChangesAsync();
            }

            var result = new RequestResult<int>
            {
                Data = 0
            };

            return result;
        }
    }

}
