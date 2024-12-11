using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;
using System.ComponentModel.DataAnnotations;

namespace MissionaryExplorationLogs.API.Requests.Commands.Missions
{
    public class UpdateMission_Command : RequestBase<int>
    {
        private readonly Mission _mission;

        public UpdateMission_Command(PlanetExplorationDbContext context, Mission mission)
            : base(context)
        {
            _mission = mission;
        }

        public override IValidator Validator =>
            new UpdateMission_Validator(DbContext, _mission);

        public override IHandler<int> Handler =>
            new UpdateMission_Handler(DbContext, _mission);
    }

    // The authorizer class is responsible for any additional authorization logic
    public class UpdateMission_Authorizer : AuthorizerBase
    {
        public UpdateMission_Authorizer(PlanetExplorationDbContext context)
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

    public class UpdateMission_Validator : ValidatorBase
    {
        private readonly Mission _mission;

        public UpdateMission_Validator(PlanetExplorationDbContext context, Mission mission)
            : base(context)
        {
            _mission = mission;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            await Task.CompletedTask;

            if (DbContext.Missions.Any(m => m.Id == _mission.Id))
            {

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
            }

            // You can also check things in the database, if needed, such as checking if a record exists
            return await ValidResultAsync();
        }
    }

    public class UpdateMission_Handler : HandlerBase<int>
    {
        private readonly Mission _mission;

        public UpdateMission_Handler(PlanetExplorationDbContext context, Mission mission)
            : base(context)
        {
            _mission = mission;
        }

        public override async Task<RequestResult<int>> HandleAsync()
        {
            var updatedMission = await DbContext.Missions.FindAsync(_mission.Id);
            if (updatedMission != null)
            {
                updatedMission.Name = _mission.Name;
                updatedMission.Date = _mission.Date;
                updatedMission.PlanetId = _mission.PlanetId;
                updatedMission.Description = _mission.Description;
                await DbContext.SaveChangesAsync();
            }

            // Return the data
            var result = new RequestResult<int>
            {
                Data = updatedMission?.Id ?? -1,
                StatusCode = HttpStatusCode.OK
            };

            return result;
        }
    }


}
