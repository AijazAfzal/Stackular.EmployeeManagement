using Serilog;
using Stackular.EmployeeManagement.Api.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureLogging();

    Log.Information("Configuring web host ({ApplicationName})...", builder.Environment.ApplicationName);

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    Log.Information("Starting web host ({ApplicationName})...", builder.Environment.ApplicationName);

    await app.RunAsync();

}

finally
{
    await Log.CloseAndFlushAsync();
}

public partial class Program
{
    protected Program()
    {
    }
}