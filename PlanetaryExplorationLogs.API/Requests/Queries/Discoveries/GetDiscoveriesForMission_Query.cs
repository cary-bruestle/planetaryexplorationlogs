using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Queries.Discoveries
{
    public class GetDiscoveriesForMission_Query : RequestBase<List<Discovery>>
    {
        private readonly int _missionId;
        public GetDiscoveriesForMission_Query(PlanetExplorationDbContext context, int missionId)
            : base(context)
        {
            _missionId = missionId;
        }

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer => new GetDiscoveriesForMission_Authorizer(DbContext);

        // The validator is optional and can be removed if not needed
        public override IValidator Validator => new GetDiscoveriesForMission_Validator(DbContext, _missionId);

        public override IHandler<List<Discovery>> Handler => new GetDiscoveriesForMission_Handler(DbContext, _missionId);
    }

    // The authorizer class is responsible for any additional authorization logic
    public class GetDiscoveriesForMission_Authorizer : AuthorizerBase
    {
        public GetDiscoveriesForMission_Authorizer(PlanetExplorationDbContext context)
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

    public class GetDiscoveriesForMission_Validator : ValidatorBase
    {
        private readonly int _missionId;

        public GetDiscoveriesForMission_Validator(PlanetExplorationDbContext context, int missionId)
            : base(context)
        {
            _missionId = missionId;
        }
        public override async Task<RequestResult> ValidateAsync()
        {
            await Task.CompletedTask;

            if (!DbContext.Missions.Any(m => m.Id == _missionId))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "You must specify a mission");
            }

            return await ValidResultAsync();
        }
    }


    public class GetDiscoveriesForMission_Handler : HandlerBase<List<Discovery>>
    {
        private readonly int _missionId;

        public GetDiscoveriesForMission_Handler(PlanetExplorationDbContext context, int missionId)
            : base(context)
        {
            _missionId = missionId;
        }

        public override async Task<RequestResult<List<Discovery>>> HandleAsync()
        {
            var discoveries = await DbContext.Discoveries
                .Where(d => d.MissionId == _missionId)
                .OrderByDescending(d => d.Id)
                .ToListAsync();

            var result = new RequestResult<List<Discovery>> { Data = discoveries };

            return result;
        }
    }
}
