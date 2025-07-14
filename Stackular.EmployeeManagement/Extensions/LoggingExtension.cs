using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using System.Globalization;

namespace Stackular.EmployeeManagement.Api.Extensions
{
    public static class LoggingExtensions
    {
        public static IHostBuilder ConfigureLogging(this IHostBuilder builder)
        {
            return builder.UseSerilog((context, services, loggerConfiguration) =>
            {
                // Base configuration from appsettings.json
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext();

                // Environment-specific sinks
                if (context.HostingEnvironment.IsDevelopment())
                {
                    // Console is configured in appsettings.Development.json
                    loggerConfiguration.WriteTo.Console(formatProvider: CultureInfo.InvariantCulture);
                }
                else
                {
                    // Use Application Insights in non-development environments
                    loggerConfiguration.WriteTo.ApplicationInsights(
                        services.GetRequiredService<TelemetryConfiguration>(),
                        TelemetryConverter.Traces);
                }
            });
        }
    }
}
