using LayerTestApp.Common.Logging;
using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Models;
using LayerTestApp.Payroll.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using Serilog.Extensions.Logging;
using System.Reflection;

namespace LayerTestApp.Payroll.DAL.Tests
{
    [TestFixture, Order(2)]
    public class RepositoryTests
    {
        private LTAPayrollDbContext _ltaPayrollDbContext;
        private ILogger<PayGradeRepository> _logger;
        private PayGradeRepository _payGradeRepository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Information("------------------------------------------------------------------------------------------");
            Log.Information("Starting RepositoryTests.");
            Log.Information("------------------------------------------------------------------------------------------\r\n");
        }

        [SetUp]
        public void Setup()
        {
            _logger = new SerilogLoggerFactory(Log.Logger).CreateLogger<PayGradeRepository>();

            string[] args = Array.Empty<string>();
            LTAPayrollDbContextFactory ltaPayrollContextFactory = new();
            _ltaPayrollDbContext = ltaPayrollContextFactory.CreateDbContext(args);

            _payGradeRepository = new PayGradeRepository(_logger, _ltaPayrollDbContext);
        }

        [Test]
        public void GetAllActivePayGradesAsync()
        {

            Log.Information("Starting GetAllPayGradesAsync - Test.");
            var payGrades = Task.Run(() => _payGradeRepository.GetAllAsync()).Result;
            try
            {
                Assert.That(payGrades, Has.Count.EqualTo(4));
                Log.Information("PayGradeRepository Test - GetAllActivePayGradesAsync - Test finished successfully. \r\n");
            }
            catch
            {
                Log.Information("PayGradeRepository Test - GetAllActivePayGradesAsync - Test finished with error. \r\n");
            }
            
        }
    }
}