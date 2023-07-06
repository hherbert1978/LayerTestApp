using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Tests
{
    abstract public class BaseTestClass
    {
        protected readonly IConfiguration _configuration;

        protected readonly LTAPayrollDbContext _context;

        protected readonly ILogger _logger;

        protected readonly IPayGradeRepository _payGradeRepository;

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

            var serviceProvider = services.BuildServiceProvider();

            _defaultSchema = _configuration["Database:DefaultSchemas:LTAPayrollSchema"];
            _context = (LTAPayrollDbContext)serviceProvider.GetServices(typeof(LTAPayrollDbContext)).First();

            _logger = ((ILoggerFactory)serviceProvider.GetServices(typeof(ILoggerFactory)).First()).CreateLogger("TestLogger");

            _payGradeRepository = (IPayGradeRepository)serviceProvider.GetServices(typeof(IPayGradeRepository)).First();
        }

    }
}
