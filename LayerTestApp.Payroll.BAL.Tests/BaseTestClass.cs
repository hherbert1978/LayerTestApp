using LayerTestApp.Payroll.DAL;
using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using Serilog;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using LayerTestApp.Payroll.BAL.ServiceContracts;

namespace LayerTestApp.Payroll.BAL.Tests
{
    abstract public class BaseTestClass
    {
        public IConfiguration Configuration;

        public LTAPayrollDbContext LtaPayrollDbContext;

        public Microsoft.Extensions.Logging.ILogger Logger;

        public IPayGradeService PayGradeService;

        public BaseTestClass() 
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            PayrollDALDependencyInjection.AddPayrollDALDI(services, Configuration);
            PayrollBALDependencyInjection.AddPayrollBALDI(services, Configuration);

            var serviceProvider = services.BuildServiceProvider();

            LtaPayrollDbContext = (LTAPayrollDbContext)serviceProvider.GetServices(typeof(LTAPayrollDbContext)).First();
            Logger = ((ILoggerFactory)serviceProvider.GetServices(typeof(ILoggerFactory)).First()).CreateLogger("TestLogger");

            PayGradeService = (IPayGradeService)serviceProvider.GetServices(typeof(IPayGradeService)).First();
        }

    }
}
