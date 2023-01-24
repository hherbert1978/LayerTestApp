using Microsoft.Extensions.Configuration;
using Serilog;

namespace LayerTestApp.Common.Logging
{
    public static class SerilogConfigurationHelper
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static ILogger ConfigureForFile(string appName,
                                            string fileName)
        {

            string logFileFolder = Configuration["SerilogLogging:LogFileFolder"];
            string logFile = Path.Combine(logFileFolder, fileName);

            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", $"{appName}")
                .WriteTo.Async(a => a.File(logFile))
                .WriteTo.Async(a => a.Console())
                .CreateLogger();

            return Log.Logger;
        }

        public static void ConfigureForElasticsearch(string appName)
        {

            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", $"{appName}")
                .WriteTo.Elasticsearch(
                    new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(
                        new Uri(Configuration["Logging:ElasticSearchURL"]))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = Serilog.Sinks.Elasticsearch.AutoRegisterTemplateVersion.ESv7,
                        IndexFormat = "MyProjectName-log-{0:yyyy.MM}"
                    }
                )
                .WriteTo.Async(a => a.Console())
                .CreateLogger();
        }
    }
}
