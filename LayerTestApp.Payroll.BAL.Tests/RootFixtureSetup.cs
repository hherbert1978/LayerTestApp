using LayerTestApp.Common.Logging;
using LayerTestApp.Payroll.DAL;
using LayerTestApp.Payroll.DAL.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LayerTestApp.Payroll.BAL.Tests
{
    [SetUpFixture]
    public class RootFixtureSetup
    {
        private readonly IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        //public LTAPayrollDbContext LtaPayrollDbContext;
        //public Microsoft.Extensions.Logging.ILogger Logger;

        [OneTimeSetUp]
        public void OneTimeRootSetup()
        {

            //var serilog = SerilogConfigurationHelper.ConfigureForFile("BALTests", "Payroll.BAL.TestLog.txt");

            //var loggerFactory = new LoggerFactory()
            //    .AddSerilog(serilog);

            //Logger = loggerFactory.CreateLogger("TestsLogger");

            //LTAPayrollDbContextFactory contextFactory = new();
            //LtaPayrollDbContext = contextFactory.CreateDbContext(null);
            var services = new ServiceCollection();

            services.AddPayrollDALDI(Configuration)
                    .AddPayrollBALDI(Configuration);

            var provider = services.BuildServiceProvider();



        }
    }
}
