using FluentValidation;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Airport
{
    public class UpdateAirportValidator : AbstractValidator<UpdateAirportCommand>
    {
        public UpdateAirportValidator()
        {
            RuleFor(airport => airport.Name).NotEmpty();
            RuleFor(airport => airport.CityName).NotEmpty();
            RuleFor(airport => airport.Latitude).InclusiveBetween(-90.0, 90.0);
            RuleFor(airport => airport.Longitude).InclusiveBetween(-180.0, 180.0);
            RuleFor(airport => airport.Base64Image).NotEmpty();
        }
    }

    public class UpdateAirportCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Base64Image { get; set; }
    }

    public class UpdateAirportCommandHandler : IRequestHandler<UpdateAirportCommand, Unit>
    {
        private readonly IAirportRepository airportRepository;
        private readonly IStorage blobStorage;

        public UpdateAirportCommandHandler(IAirportRepository airportRepository, IStorage blobStorage)
        {
            this.airportRepository = airportRepository;
            this.blobStorage = blobStorage;
        }

        public async Task<Unit> Handle(UpdateAirportCommand request, CancellationToken cancellationToken)
        {
            var airport = await airportRepository.GetAsync(request.Id, cancellationToken);
            var url = await blobStorage.UploadAirportImage(request.Base64Image);

            airport.Name = request.Name;
            airport.CityName = request.CityName;
            airport.Latitude = request.Latitude;
            airport.Longitude = request.Longitude;
            airport.BlobUrl = url;

            airportRepository.Update(airport);

            return Unit.Value;
        }
    }
}
