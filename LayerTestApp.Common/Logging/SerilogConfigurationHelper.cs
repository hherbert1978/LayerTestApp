using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Filters;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

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
            string logFileFramework = Path.Combine(logFileFolder, "Payroll.DAL.TestLog.Debug.txt");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", $"{appName}")    
             
                .WriteTo.Conditional(ev => ev.Level == Serilog.Events.LogEventLevel.Information, l => l.Logger(
                                                                                                             a => a.Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore"))
                                                                                                                   .WriteTo.File(logFile, shared: true)))
                .WriteTo.Conditional(ev => ev.Level == Serilog.Events.LogEventLevel.Information || 
                                           ev.Level == Serilog.Events.LogEventLevel.Warning || 
                                           ev.Level == Serilog.Events.LogEventLevel.Debug, 
                                     l => l.Logger(a => a.Filter.ByIncludingOnly(Matching.FromSource("Microsoft.EntityFrameworkCore"))
                                                         .WriteTo.File(logFileFramework, shared: true)))
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
