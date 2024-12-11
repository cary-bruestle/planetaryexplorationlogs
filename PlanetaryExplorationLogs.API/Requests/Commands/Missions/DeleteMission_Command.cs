using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Missions
{
    public class DeleteMission_Command : RequestBase<int>
    {
        private readonly int _missionId;

        public DeleteMission_Command(PlanetExplorationDbContext context, int missionId)
            : base(context)
        {
            _missionId = missionId;
        }

        // The authorizer, validator, and handler run in that order. If any fail, the query will not be executed.

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer => new DeleteMission_Authorizer(DbContext);

        // The validator is optional and can be removed if not needed
        public override IValidator Validator =>
            new DeleteMission_Validator(DbContext, _missionId);

        // The handler is mandatory to have for every command
        public override IHandler<int> Handler =>
            new DeleteMission_Handler(DbContext, _missionId);
    }

    // The authorizer class is responsible for any additional authorization logic
    public class DeleteMission_Authorizer : AuthorizerBase
    {
        public DeleteMission_Authorizer(PlanetExplorationDbContext context)
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
    public class DeleteMission_Validator : ValidatorBase
    {
        private readonly int _missionId;

        public DeleteMission_Validator(PlanetExplorationDbContext context, int missionId)
            : base(context)
        {
            _missionId = missionId;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            await Task.CompletedTask;

            if (!await DbContext.Missions.AnyAsync(m => m.Id == _missionId))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "No mission exists with the given ID.");
            }

            return await ValidResultAsync();
        }
    }

    // The handler class is responsible for executing the query
    public class DeleteMission_Handler : HandlerBase<int>
    {
        private readonly int _missionId;

        public DeleteMission_Handler(PlanetExplorationDbContext context, int id)
            : base(context)
        {
            _missionId = id;
        }

        public override async Task<RequestResult<int>> HandleAsync()
        {
            var mission = await DbContext.Missions.FindAsync(_missionId);
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
