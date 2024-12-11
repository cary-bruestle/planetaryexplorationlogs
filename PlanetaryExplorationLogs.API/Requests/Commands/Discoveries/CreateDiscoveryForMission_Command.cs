using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Requests.Commands.Planets;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Numerics;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Missions
{

    public class CreateDiscoveryForMission_Command : RequestBase<int>
    {
        private readonly DiscoveryFormDto _discovery;
        private readonly int _missionId;

        public CreateDiscoveryForMission_Command(PlanetExplorationDbContext context, int missionid, DiscoveryFormDto discovery)
            : base(context)
        {
            _missionId = missionid;
            _discovery = discovery;
        }

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer => new CreateDiscoveryForMission_Authorizer(DbContext);

        public override IValidator Validator =>
            new CreateDiscoveryForMission_Validator(DbContext, _missionId, _discovery);

        public override IHandler<int> Handler =>
            new CreateDiscoveryForMission_Handler(DbContext, _missionId, _discovery);

    }

    // The authorizer class is responsible for any additional authorization logic
    public class CreateDiscoveryForMission_Authorizer : AuthorizerBase
    {
        public CreateDiscoveryForMission_Authorizer(PlanetExplorationDbContext context)
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
    public class CreateDiscoveryForMission_Validator : ValidatorBase
    {
        private readonly DiscoveryFormDto _discovery;
        private readonly int _missionId;

        public CreateDiscoveryForMission_Validator(PlanetExplorationDbContext context, int missionId, DiscoveryFormDto discovery)
            : base(context)
        {
            _missionId = missionId;
            _discovery = discovery;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            // Obviously, this is dummy validation logic. Replace it with your own.
            await Task.CompletedTask;

            if (string.IsNullOrEmpty(_discovery.Name.Trim()))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "The discovery must have a name.");
            }

            if (!DbContext.Missions.Any(m => m.Id == _missionId))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "The discovery must be for a mission.");
            }

            if (!DbContext.DiscoveryTypes.Any(t => t.Id == _discovery.DiscoveryTypeId))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "Invalid or mission type.");
            }

            return await ValidResultAsync();
        }
    }

    // The handler class is responsible for executing the query
    public class CreateDiscoveryForMission_Handler : HandlerBase<int>
    {

        private readonly DiscoveryFormDto _discovery;
        private readonly int _missionId;

        public CreateDiscoveryForMission_Handler(PlanetExplorationDbContext context, int missionId, DiscoveryFormDto discovery)
            : base(context)
        {
            _discovery = discovery;
            _missionId = missionId;
        }

        public override async Task<RequestResult<int>> HandleAsync()
        {
            var newDiscovery = new Discovery
            {
                Name = _discovery.Name,
                MissionId = _missionId,
                DiscoveryTypeId = _discovery.DiscoveryTypeId,
                Description = _discovery.Description,
                Location = _discovery.Location
            };

            await DbContext.Discoveries.AddAsync(newDiscovery);
            await DbContext.SaveChangesAsync();

            var result = new RequestResult<int>
            {
                Data = newDiscovery.Id,
                StatusCode = HttpStatusCode.Created
            };

            return result;
        }
    }

}
