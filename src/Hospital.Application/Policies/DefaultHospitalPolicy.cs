using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace Hospital.Application.Policies;
public static class DefaultHospitalPolicy
{
    public static AsyncRetryPolicy DefaultRetryPolicy(ILogger logger)
    {
        return Policy
            .Handle<Exception>()  // Handle all exceptions, customize as needed
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1.3, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    logger.LogWarning($"⚠️ Retrying due to exception: {exception.Message}. Attempt {retryCount} will occur in {timeSpan.TotalSeconds} seconds.");
                });
    }

    public static AsyncCircuitBreakerPolicy DefaultCircuitBreakerPolicy(ILogger logger)
    {
        return Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(2),
                onBreak: (exception, timespan) =>
                {
                    logger.LogError($"⚠️ Circuit breaker triggered due to exception: {exception.Message}. Circuit will stay open for {timespan.TotalSeconds} seconds.");
                },
                onReset: () =>
                {
                    logger.LogInformation("✅ Circuit breaker has been reset.");
                });
    }

    public static AsyncPolicy GetDefaultPolicy(ILogger logger)
    {
        return DefaultRetryPolicy(logger).WrapAsync(DefaultCircuitBreakerPolicy(logger));
    }
}