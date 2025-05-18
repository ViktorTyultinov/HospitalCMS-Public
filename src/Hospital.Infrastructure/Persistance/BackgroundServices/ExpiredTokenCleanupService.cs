using Hospital.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hospital.Infrastructure.Persistance.BackgroundServices;

public class ExpiredTokenCleanupService(IServiceProvider provider, ILogger<ExpiredTokenCleanupService> logger) : BackgroundService
{
    private readonly IServiceProvider _provider = provider;
    private readonly ILogger _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;
            var nextRun = DateTime.Today.AddDays(now.Hour >= 3 ? 1 : 0).AddHours(1);
            var delay = nextRun - now;

            _logger.LogInformation($"Next refresh token cleanup will run after {delay}");

            if (delay < TimeSpan.Zero)
                delay = TimeSpan.Zero;

            await Task.Delay(delay, stoppingToken);

            try
            {
                using var scope = _provider.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var numberOfDeletedTokens = await unitOfWork.RefreshTokens.RemoveExpiredTokens();
                _logger.LogInformation("Refresh tokens cleaned. Removed {Count} tokens", numberOfDeletedTokens);

                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during refresh token cleanup");
            }
        }
    }
}
