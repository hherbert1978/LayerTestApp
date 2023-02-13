using LayerTestApp.Common.Logging;
using LayerTestApp.Payroll.DAL.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Payroll.DAL.Tests
{
    abstract public class BaseTestClass
    {
        public readonly IConfiguration Configuration;

        public readonly LTAPayrollDbContext LtaPayrollDbContext;

        public readonly Microsoft.Extensions.Logging.ILogger Logger;
        
        public BaseTestClass() 
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            PayrollDALDependencyInjection.AddPayrollDALDI(services, Configuration);
            var serviceProvider = services.BuildServiceProvider();
            LtaPayrollDbContext = (LTAPayrollDbContext)serviceProvider.GetServices(typeof(LTAPayrollDbContext)).First();

            //var serilog = SerilogConfigurationHelper.ConfigureForFile("DALTests", "Payroll.DAL.TestLog.txt");
            //var loggerFactory = new LoggerFactory()
            //    .AddSerilog(serilog);

            //Logger = loggerFactory.CreateLogger("TestsLogger");
        }

    }
}
