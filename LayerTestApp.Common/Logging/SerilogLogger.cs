using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Serilog;
using System.Globalization;

namespace LayerTestApp.Common.Logging
{
    public class SerilogLogger : ISerilogLogger
    {

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "./"))
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public void LogInformation(LoggingLevels eventLevel, string info, Exception? exInfo = null, params object[] values)
        {
            Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));

            var loggingLevelSwitch = new Serilog.Core.LoggingLevelSwitch();
            var defaultLogEventLevel = Serilog.Events.LogEventLevel.Debug;
            string logLevel = Configuration.GetSection("DefaultLoggingLevel").Value = string.Empty;

            try
            {
                defaultLogEventLevel = (Serilog.Events.LogEventLevel)Enum.Parse(
                    typeof(Serilog.Events.LogEventLevel),
                    logLevel

                );
            }
            catch (Exception ex)
            {
                var result = $"Common.Logging.SerilogLogger.LogInformation: exception getting default logging level, defaulted to Debug Level. logLevel:{logLevel}, exception:{ex}";
                System.Diagnostics.Debug.WriteLine(result);
            }

            loggingLevelSwitch.MinimumLevel = defaultLogEventLevel;

            string file = Configuration.GetSection("LogFile").Value = string.Empty;


            using (var log = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(loggingLevelSwitch)
                .WriteTo.File(file, rollingInterval: RollingInterval.Day)
                .CreateLogger())
            {
                if (log.IsEnabled(defaultLogEventLevel))
                {
                    if (exInfo != null)
                    {
                        log.Write(defaultLogEventLevel, exInfo, info, values);
                    }
                    else
                    {
                        log.Write(defaultLogEventLevel, info, values);
                    }
                }
                else
                {
                    var result = $"Logger: eventLevel:{nameof(logLevel)} is not enabled";
                    System.Diagnostics.Debug.WriteLine(result);
                }

            }
        }
    }
}
