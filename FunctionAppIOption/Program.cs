using FunctionAppIOption.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddOptions<AppConfigurations>()
                .Configure<IConfiguration>((options, configuration) => configuration.Bind(options))
                .ValidateDataAnnotations();
        services.AddOptions<AppSettings>()
                .Configure<IConfiguration>((options, configuration) => configuration.Bind(options))
                .ValidateDataAnnotations();
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .Build();

host.Run();
