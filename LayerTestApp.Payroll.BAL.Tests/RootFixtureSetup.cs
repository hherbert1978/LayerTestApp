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

        [OneTimeSetUp]
        public void OneTimeRootSetup()
        {            
            var services = new ServiceCollection();

            services.AddPayrollDALDI(Configuration)
                    .AddPayrollBALDI(Configuration);

            var provider = services.BuildServiceProvider();
        }
    }
}
