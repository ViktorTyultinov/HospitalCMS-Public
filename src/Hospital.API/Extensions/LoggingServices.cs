using OpenTelemetry.Resources;
using Serilog;

namespace Hospital.API.Extensions;

public static class LoggingSetup
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        // OpenTelemetry Logging
        builder.Logging.ClearProviders();
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;
            options.IncludeScopes = true;
            options.ParseStateValues = true;
            options.SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(builder.Environment.ApplicationName));
        });

        // Serilog
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .Enrich.WithCorrelationId()
            .Filter.ByExcluding(logEvent =>
                logEvent.Properties.ContainsKey("SourceContext") &&
                (
                    logEvent.Properties["SourceContext"].ToString().Contains("Microsoft.EntityFrameworkCore.Database.Command") ||
                    logEvent.Properties["SourceContext"].ToString().Contains("Microsoft.EntityFrameworkCore.Query") ||
                    logEvent.Properties["SourceContext"].ToString().Contains("Microsoft.AspNetCore.Server.Kestrel")
                )
            )
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{CorrelationId}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.Seq("http://seq:5341")
            .CreateLogger();

        builder.Host.UseSerilog();
    }
}
