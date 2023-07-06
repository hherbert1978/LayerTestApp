using LayerTestApp.Payroll.BAL.ServiceContracts;
using LayerTestApp.Payroll.DAL;
using LayerTestApp.Payroll.DAL.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.BAL.Tests
{
    abstract public class BaseTestClass
    {
        protected readonly IConfiguration _configuration;

        protected readonly LTAPayrollDbContext _context;

        protected readonly ILogger _logger;

        protected readonly IPayGradeService _payGradeService;

        protected readonly string _defaultSchema;

        public BaseTestClass()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            services.AddPayrollDALDI(_configuration);
            services.AddPayrollBALDI();

            var serviceProvider = services.BuildServiceProvider();

            _defaultSchema = _configuration["Database:DefaultSchemas:LTAPayrollSchema"];
            _context = (LTAPayrollDbContext)serviceProvider.GetServices(typeof(LTAPayrollDbContext)).First();
            _logger = ((ILoggerFactory)serviceProvider.GetServices(typeof(ILoggerFactory)).First()).CreateLogger("TestLogger");

            _payGradeService = (IPayGradeService)serviceProvider.GetServices(typeof(IPayGradeService)).First();
        }

    }
}
