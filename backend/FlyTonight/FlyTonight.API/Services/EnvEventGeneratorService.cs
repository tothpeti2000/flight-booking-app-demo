using FlyTonight.Application.Services;
using MediatR;

namespace FlyTonight.API.Services
{
    public class EnvEventGeneratorService : IHostedService, IDisposable
    {
        private int executionTimeMinutes = 1;
        private readonly ILogger<EnvEventGeneratorService> _logger;
        private Timer? _timer = null;
        private readonly IServiceScopeFactory serviceProvider;

        public EnvEventGeneratorService(ILogger<EnvEventGeneratorService> logger, IServiceScopeFactory serviceProvider)
        {
            _logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Environment Event Generator Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.FromMinutes(2), TimeSpan.FromDays(executionTimeMinutes));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new EnvEventCommand() { EventDate = DateTime.Today });
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Environment Event Generator Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
