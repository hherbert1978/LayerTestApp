using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Entities;
using LayerTestApp.Payroll.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;

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

        [Test, Order(1)]
        public void GetAllPayGradesAsync()
        {

            Log.Information("Starting GetAllPayGradesAsync - Test.");
            var payGrades = Task.Run(() => _payGradeRepository.GetAllAsync(false)).Result;
            try
            {
                Assert.That(payGrades, Has.Count.EqualTo(5));
                Log.Information("PayGradeRepository Test - GetAllPayGradesAsync - Test finished successfully. \r\n");
            }
            catch
            {
                Log.Information("PayGradeRepository Test - GetAllPayGradesAsync - Test finished with error. \r\n");
            }

        }

        [Test, Order(2)]
        public void GetAllActivePayGradesAsync()
        {

            Log.Information("Starting GetAllActivePayGradesAsync - Test.");
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

        [Test, Order(3)]
        public void CreateNewPayGradeAsync()
        {

            Log.Information("Starting CreateNewPayGradeAsync - Test.");

            var payGrade = new PayGrade("Neue Gehaltsgruppe");
            payGrade = Task.Run(() => _payGradeRepository.AddAsync(payGrade)).Result;

            try
            {
                Assert.That(payGrade.PayGradeId, Is.Not.Null);
                Assert.That(payGrade.PayGradeId, Is.EqualTo(7));
                Log.Information("PayGradeRepository Test - CreateNewPayGradeAsync - Test finished successfully. \r\n");
            }
            catch
            {
                Log.Information("PayGradeRepository Test - CreateNewPayGradeAsync - Test finished with error. \r\n");
            }

        }


    }
}