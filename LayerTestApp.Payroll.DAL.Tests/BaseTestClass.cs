using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Tests
{
    abstract public class BaseTestClass
    {
        public IConfiguration Configuration;

        public LTAPayrollDbContext LtaPayrollDbContext;

        public Microsoft.Extensions.Logging.ILogger Logger;

        public IPayGradeRepository PayGradeRepository;

        public string DefaultSchema;

        public BaseTestClass()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            PayrollDALDependencyInjection.AddPayrollDALDI(services, Configuration);
            var serviceProvider = services.BuildServiceProvider();

            DefaultSchema = Configuration["Database:DefaultSchemas:LTAPayrollSchema"];
            LtaPayrollDbContext = (LTAPayrollDbContext)serviceProvider.GetServices(typeof(LTAPayrollDbContext)).First();

            Logger = ((ILoggerFactory)serviceProvider.GetServices(typeof(ILoggerFactory)).First()).CreateLogger("TestLogger");

            PayGradeRepository = (IPayGradeRepository)serviceProvider.GetServices(typeof(IPayGradeRepository)).First();
        }

    }
}
