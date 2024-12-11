using PlanetaryExplorationLogs.API.Data.Context;
using PlanetaryExplorationLogs.API.Data.DTO;
using PlanetaryExplorationLogs.API.Data.Models;
using PlanetaryExplorationLogs.API.Utility.Patterns;
using System.Net;
using static PlanetaryExplorationLogs.API.Utility.Patterns.CommandQuery;

namespace PlanetaryExplorationLogs.API.Requests.Commands.Planets
{
    public class AddPlanet_Command : RequestBase<int>
    {
        private readonly PlanetFormDto _planet;

        public AddPlanet_Command(PlanetExplorationDbContext context, PlanetFormDto planet)
            : base(context)
        {
            _planet = planet;
        }

        public override IValidator Validator =>
            new AddPlanet_Validator(DbContext, _planet);

        public override IHandler<int> Handler =>
            new AddPlanet_Handler(DbContext, _planet);
    }

    public class AddPlanet_Validator : ValidatorBase
    {
        private readonly PlanetFormDto _planet;

        public AddPlanet_Validator(PlanetExplorationDbContext context, PlanetFormDto planet)
            : base(context)
        {
            _planet = planet;
        }

        public override async Task<RequestResult> ValidateAsync()
        {
            // Obviously, this is dummy validation logic. Replace it with your own.
            await Task.CompletedTask;

            if (string.IsNullOrEmpty(_planet.Name.Trim()))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "The planet must have a name.");
            }

            if (string.IsNullOrEmpty(_planet.Type.Trim()))
            {
                return await InvalidResultAsync(
                    HttpStatusCode.BadRequest,
                    "The planet must have a type.");
            }

            // You can also check things in the database, if needed, such as checking if a record exists
            return await ValidResultAsync();
        }
    }

    public class AddPlanet_Handler : HandlerBase<int>
    {
        private readonly PlanetFormDto _planet;

        public AddPlanet_Handler(PlanetExplorationDbContext context, PlanetFormDto planet)
            : base(context)
        {
            _planet = planet;
        }

        public override async Task<RequestResult<int>> HandleAsync()
        {
            var newPlanet = new Planet
            {
                Climate = _planet.Climate,
                Name = _planet.Name,
                Population = _planet.Population,
                Terrain = _planet.Terrain,
                Type = _planet.Type
            };

            await DbContext.Planets.AddAsync(newPlanet);
            await DbContext.SaveChangesAsync();

            var result = new RequestResult<int>
            {
                Data = newPlanet.Id,
                StatusCode = HttpStatusCode.Created
            };

            return result;
        }
    }
}
