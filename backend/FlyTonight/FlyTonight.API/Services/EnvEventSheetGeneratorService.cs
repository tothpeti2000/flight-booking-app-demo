using MediatR;
using Cronos;
using Timer = System.Timers.Timer;
using FlyTonight.Application.Services;

namespace FlyTonight.API.Services
{
    public class EnvEventSheetGeneratorService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger<EnvEventSheetGeneratorService> _logger;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo = TimeZoneInfo.Local;
        private readonly IMediator _mediator;

        public EnvEventSheetGeneratorService(ILogger<EnvEventSheetGeneratorService> logger, IMediator mediator)
        {
            _expression = CronExpression.Parse("0 23 ? * SUN");
            _logger = logger;
            _mediator = mediator;
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken);
        }

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (!next.HasValue)
                return;

            var delay = next.Value - DateTimeOffset.Now;
            if (delay.TotalMilliseconds <= 0)
            {
                await ScheduleJob(cancellationToken);
                return;
            }
            _timer = new Timer(delay.TotalMilliseconds);
            _timer.Elapsed += async (sender, args) =>
            {
                _timer.Dispose();
                _timer = null;

                if (!cancellationToken.IsCancellationRequested)
                {
                    await DoWork(cancellationToken);
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    await ScheduleJob(cancellationToken);
                }
            };
            _timer.Start();
        }

        public virtual async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Weekly event reports are beign generated...");

            await _mediator.Send(new EventSpreadsheetCommand());
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
