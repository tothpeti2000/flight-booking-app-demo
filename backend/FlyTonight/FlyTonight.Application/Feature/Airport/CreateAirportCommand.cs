using FluentValidation;
using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.Airport
{
    public class CreateAirportValidator : AbstractValidator<CreateAirportCommand>
    {
        public CreateAirportValidator()
        {
            RuleFor(airport => airport.Name).NotEmpty();
            RuleFor(airport => airport.CityName).NotEmpty();
            RuleFor(airport => airport.Latitude).InclusiveBetween(-90.0, 90.0);
            RuleFor(airport => airport.Longitude).InclusiveBetween(-180.0, 180.0);
            RuleFor(airport => airport.Base64Image).NotEmpty();
        }
    }

    public class CreateAirportCommand : IRequest
    {
        public string Name { get; set; }
        public string CityName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Base64Image { get; set; }
    }

    public class CreateAirportCommandHandler : IRequestHandler<CreateAirportCommand, Unit>
    {
        private readonly IAirportRepository airportRepository;
        private readonly IStorage blobStorage;

        public CreateAirportCommandHandler(IAirportRepository airportRepository, IStorage blobStorage)
        {
            this.airportRepository = airportRepository;
            this.blobStorage = blobStorage;
        }

        public async Task<Unit> Handle(CreateAirportCommand request, CancellationToken cancellationToken)
        {
            var url = await blobStorage.UploadAirportImage(request.Base64Image);
            airportRepository.Add(new()
            {
                Name = request.Name,
                CityName = request.CityName,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                BlobUrl = url,
            });

            return Unit.Value;
        }
    }
}
