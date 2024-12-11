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

    public class CreateMission_Command : RequestBase<int>
    {
        private readonly MissionFormDto _mission;

        public CreateMission_Command(PlanetExplorationDbContext context, MissionFormDto mission)
            : base(context)
        {
            _mission = mission;
        }

        // The authorizer is optional and can be removed if not needed
        public override IAuthorizer Authorizer => new CreateMission_Authorizer(DbContext);

        public override IValidator Validator =>
            new CreateMission_Validator(DbContext, _mission);

        public override IHandler<int> Handler =>
            new CreateMission_Handler(DbContext, _mission);

    }

    // The authorizer class is responsible for any additional authorization logic
    public class CreateMission_Authorizer : AuthorizerBase
    {
        public CreateMission_Authorizer(PlanetExplorationDbContext context)
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
    public class CreateMission_Validator : ValidatorBase
    {
        private readonly MissionFormDto _mission;

        public CreateMission_Validator(PlanetExplorationDbContext context, MissionFormDto mission)
            : base(context)
        {
            _mission = mission;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            // Obviously, this is dummy validation logic. Replace it with your own.
            await Task.CompletedTask;

            if (string.IsNullOrEmpty(_mission.Name.Trim()))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "The mission must have a name.");
            }

            if (!DbContext.Planets.Any(p => p.Id == _mission.PlanetId))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "The mission must be for a planet.");
            }

            // You can also check things in the database, if needed, such as checking if a record exists
            return await ValidResultAsync();
        }
    }

    // The handler class is responsible for executing the query
    public class CreateMission_Handler : HandlerBase<int>
    {
        private readonly MissionFormDto _mission;

        public CreateMission_Handler(PlanetExplorationDbContext context, MissionFormDto mission)
            : base(context)
        {
            _mission = mission;
        }

        public override async Task<RequestResult<int>> HandleAsync()
        {
            var newMission = new Mission
            {
                Name = _mission.Name,
                Date = _mission.Date,
                PlanetId = _mission.PlanetId,
                Description = _mission.Description
            };

            await DbContext.Missions.AddAsync(newMission);
            await DbContext.SaveChangesAsync();

            var result = new RequestResult<int>
            {
                Data = newMission.Id,
                StatusCode = HttpStatusCode.Created
            };

            return result;
        }
    }

}
