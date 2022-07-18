using FlyTonight.Application.Interfaces;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using FlyTonight.Domain.Models.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace FlyTonight.Application.Services
{
    public class EnvEventCommand : IRequest
    {
        public DateTime EventDate { get; set; } = DateTime.Parse("2022-08-12");
    }

    public class EnvEventCommandHandler : IRequestHandler<EnvEventCommand, Unit>
    {
        private readonly IFlightRepository flightRepository;
        private readonly IEmailService emailService;
        private readonly IOrderRepository orderRepository;
        private readonly UserManager<User> userManager;
        private readonly ILogger<EnvEventCommandHandler> logger;
        private readonly Random rnd;

        public EnvEventCommandHandler(
            IFlightRepository flightRepository,
            IEmailService emailService,
            IOrderRepository orderRepository,
            UserManager<User> userManager,
            ILogger<EnvEventCommandHandler> logger)
        {
            this.flightRepository = flightRepository;
            this.emailService = emailService;
            this.orderRepository = orderRepository;
            this.userManager = userManager;
            this.logger = logger;
            rnd = new Random();
        }

        public async Task<Unit> Handle(EnvEventCommand request, CancellationToken cancellationToken)
        {
            var flights = await flightRepository.GetAllByDate(request.EventDate);

            await GenerateEvents(flights);

            return Unit.Value;
        }


        private Dictionary<Type, int> GetEvents()
        {
            var events = typeof(EnvEventBase)
                .Assembly
                .GetTypes()
                .Where(t => t.BaseType == typeof(EnvEventBase))
                .ToDictionary(t => t, t => t.GetCustomAttribute<EventChanceAttribute>()?.Percent ?? 0);

            int totalChance = events.Sum(kv => kv.Value);
            if (totalChance > 100)
            {
                throw new ArgumentException($"Total event chance: {totalChance} exceeds 100%");
            }

            return events;
        }

        public EnvEventBase GenerateEvent(Dictionary<Type, int> eventTypes, Flight flight)
        {
            var number = rnd.Next(100);
            
            var counter = 0;
            foreach (var envEvent in eventTypes)
            {
                if (counter <= number && number < (counter + envEvent.Value))
                {
                    return (EnvEventBase)Activator.CreateInstance(envEvent.Key, new object[] { flight });
                }
                counter += envEvent.Value;
            }
            return null;
        }

        public async Task GenerateEvents(IEnumerable<Flight> flights)
        {
            logger.LogInformation("Starting EnvEvent generation");

            var events = GetEvents();

            foreach (var flight in flights)
            {
                if (flight.EnvEvent is null)
                {
                    var envEvent = GenerateEvent(events, flight);

                    if (envEvent is not null)
                    {
                        flight.EnvEvent = envEvent;
                        logger.LogInformation("Generated {0} event for fligth {1}", envEvent.GetType().Name, flight.Id);
                        await SendEmailsForFlight(flight, envEvent);
                    }
                }
            }

            logger.LogInformation("EnvEvent generation ended");
        }

        public async Task SendEmailsForFlight(Flight flight, EnvEventBase envEvent)
        {
            var orders = await orderRepository.GetAllForFlightAsync(flight.Id);

            foreach (var order in orders)
            {
                var user = await userManager.FindByIdAsync(order.UserId);

                await emailService.SendDelayEventEmail(user.Email, flight.Id.ToString(), envEvent);
            }
        }
    }
}
