using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Repositories;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Filters;

namespace LayerTestApp.Payroll.DAL
{
    public static class PayrollDALDependencyInjection
    {
        public static IServiceCollection AddPayrollDALDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLTAPayrollDatabase(configuration);
            services.AddLTAPayrollRepositories();
            services.AddLogging(configuration);

            return services;
        }

        private static void AddLTAPayrollDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Database:ConnectionStrings:LTAPayrollDBConnection"];
            services.AddDbContext<LTAPayrollDbContext>(opt =>
                opt.UseNpgsql(connectionString));

        }

        private static void AddLTAPayrollRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPayGradeRepository, PayGradeRepository>();

        }

        private static void AddLogging(this IServiceCollection services, IConfiguration configuration)
        {
            string logFileFolder = configuration["SerilogLogging:LogFileFolder"];
            string logFileName = configuration["SerilogLogging:LogFileName"];
            string debugLogFileName = configuration["SerilogLogging:DebugLogFileName"];

            string logFile = Path.Combine(logFileFolder, logFileName);
            string debugLogFile = Path.Combine(logFileFolder, debugLogFileName);

            Serilog.ILogger serilog = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Conditional(ev => ev.Level == Serilog.Events.LogEventLevel.Information ||
                                           ev.Level == Serilog.Events.LogEventLevel.Warning ||
                                           ev.Level == Serilog.Events.LogEventLevel.Debug,
                                     l => l.Logger(
                                         a => a.Filter.ByIncludingOnly(Matching.FromSource("Microsoft.EntityFrameworkCore"))
                                               .WriteTo.File(debugLogFile, shared: true)))
                .WriteTo.Conditional(ev => ev.Level == Serilog.Events.LogEventLevel.Information ||
                                           ev.Level == Serilog.Events.LogEventLevel.Warning ||
                                           ev.Level == Serilog.Events.LogEventLevel.Debug,
                                     l => l.Logger(
                                         a => a.Filter.ByExcluding(Matching.FromSource("Microsoft.EntityFrameworkCore"))
                                               .WriteTo.File(logFile, shared: true)))
                .WriteTo.Async(a => a.Console())
                .CreateLogger();

            services.AddLogging(x => x.AddSerilog(serilog));
            services.AddSingleton(typeof(Microsoft.Extensions.Logging.ILogger), typeof(Logger<PayGradeRepository>));
        }
    }
}
