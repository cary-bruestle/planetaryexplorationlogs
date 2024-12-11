//// Paste these using statements at the top of the file and uncomment them
using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using PlanetaryExplorationLogs.API.Data.Context;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;
using PlanetaryExplorationLogs.API.Data.Models;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Missions
{

    public class GetMission_Query : RequestBase<Mission>
    {
        private readonly int _missionId;

        public GetMission_Query(PlanetExplorationDbContext context, int missionId)
            : base(context)
        {
            _missionId = missionId;
        }

        // The authorizer, validator, and handler run in that order. If any fail, the query will not be executed.

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer => new GetMission_Authorizer(DbContext);

        // The validator is optional and can be removed if not needed
        public override IValidator Validator => new GetMission_Validator(DbContext, _missionId);

        // The handler is mandatory to have for every query
        public override IHandler<Mission> Handler => new GetMission_Handler(DbContext, _missionId);
    }

    // The handler class is responsible for executing the query
    public class GetMission_Handler : HandlerBase<Mission>
    {
        private readonly int _missionId;

        public GetMission_Handler(PlanetExplorationDbContext context, int missionId)
            : base(context)
        {
            _missionId = missionId;
        }

        public override async Task<RequestResult<Mission>> HandleAsync()
        {
            // Obviously, this is a dummy query. Replace it with your own.
            // Write your query here
            var someMission = await DbContext.Missions.Where(u => u.Id.Equals(_missionId)).FirstAsync();

            // Return the data
            var result = new RequestResult<Mission> { Data = someMission };

            return result;
        }
    }

    // The authorizer class is responsible for any additional authorization logic
    public class GetMission_Authorizer : AuthorizerBase
    {
        public GetMission_Authorizer(PlanetExplorationDbContext context)
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
    public class GetMission_Validator : ValidatorBase
    {
        private readonly int _missionId;

        public GetMission_Validator(PlanetExplorationDbContext context, int missionId)
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

}
