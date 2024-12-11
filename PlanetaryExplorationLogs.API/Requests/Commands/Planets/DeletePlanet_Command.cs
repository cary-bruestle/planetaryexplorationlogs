using Microsoft.EntityFrameworkCore;
using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Planets
{
    public class DeletePlanet_Command : RequestBase<int>
    {
        private readonly int _planetId;

        public DeletePlanet_Command(PlanetExplorationDbContext context, int planetId)
            : base(context)
        {
            _planetId = planetId;
        }

        public override IValidator Validator =>
            new DeletePlanet_Validator(DbContext, _planetId);

        public override IHandler<int> Handler =>
            new DeletePlanet_Handler(DbContext, _planetId);
    }

    public class DeletePlanet_Validator : ValidatorBase
    {
        private readonly int _planetId;

        public DeletePlanet_Validator(PlanetExplorationDbContext context, int planetId)
            : base(context)
        {
            _planetId = planetId;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            await Task.CompletedTask;

            if (!await DbContext.Planets.AnyAsync(p => p.Id == _planetId))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "No planet exists with the given ID.");
            }

            return await ValidResultAsync();
        }
    }

    public class DeletePlanet_Handler : HandlerBase<int>
    {
        private readonly int _planetId;

        public DeletePlanet_Handler(PlanetExplorationDbContext context, int planetId)
            : base(context)
        {
            _planetId = planetId;
        }

        public override async Task<RequestResult<int>> HandleAsync()
        {
            var planet = await DbContext.Planets.FindAsync(_planetId);
            if (planet != null)
            {
                DbContext.Planets.Remove(planet);
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
