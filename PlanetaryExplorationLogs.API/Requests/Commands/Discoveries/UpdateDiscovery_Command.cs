using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Discoveries
{
    public class UpdateDiscovery_Command : RequestBase<int>
    {
        private readonly Discovery _discovery;

        public UpdateDiscovery_Command(PlanetExplorationDbContext context, Discovery discovery)
            : base(context)
        {
            _discovery = discovery;
        }

        public override IValidator Validator =>
            new UpdateDiscovery_Validator(DbContext, _discovery);

        public override IHandler<int> Handler =>
            new UpdateDiscovery_Handler(DbContext, _discovery);
    }

    // The authorizer class is responsible for any additional authorization logic
    public class UpdateDiscovery_Authorizer : AuthorizerBase
    {
        public UpdateDiscovery_Authorizer(PlanetExplorationDbContext context)
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

    public class UpdateDiscovery_Validator : ValidatorBase
    {
        private readonly Discovery _discovery;

        public UpdateDiscovery_Validator(PlanetExplorationDbContext context, Discovery discovery)
            : base(context)
        {
            _discovery = discovery;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            await Task.CompletedTask;

            if (DbContext.Discoveries.Any(m => m.Id == _discovery.Id))
            {

                if (string.IsNullOrEmpty(_discovery.Name.Trim()))
                {
                    return await InvalidResultAsync(
                        HttpStatusCode.BadRequest,
                        "The discovery must have a name.");
                }

                if (!DbContext.Missions.Any(m => m.Id == _discovery.MissionId))
                {
                    return await InvalidResultAsync(
                        HttpStatusCode.BadRequest,
                        "The discovery must be for a mission.");
                }
                if (!DbContext.DiscoveryTypes.Any(t => t.Id == _discovery.DiscoveryTypeId))
                {
                    return await InvalidResultAsync(
                        HttpStatusCode.BadRequest,
                        "The discovery must have a vaild type.");
                }

            }

            // You can also check things in the database, if needed, such as checking if a record exists
            return await ValidResultAsync();
        }
    }

    public class UpdateDiscovery_Handler : HandlerBase<int>
    {
        private readonly Discovery _discovery;

        public UpdateDiscovery_Handler(PlanetExplorationDbContext context, Discovery discovery)
            : base(context)
        {
            _discovery = discovery;
        }

        public override async Task<RequestResult<int>> HandleAsync()
        {
            var updatedDiscovery = await DbContext.Discoveries.FindAsync(_discovery.Id);
            if (updatedDiscovery != null)
            {
                updatedDiscovery.Name = _discovery.Name;
                updatedDiscovery.DiscoveryTypeId = _discovery.DiscoveryTypeId;
                updatedDiscovery.Description = _discovery.Description;
                updatedDiscovery.Location = _discovery.Location;

                await DbContext.SaveChangesAsync();
            }

            // Return the data
            var result = new RequestResult<int>
            {
                Data = updatedDiscovery?.Id ?? -1,
                StatusCode = HttpStatusCode.OK
            };

            return result;
        }
    }
}
