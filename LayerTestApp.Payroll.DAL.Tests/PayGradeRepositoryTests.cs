using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Models;
using LayerTestApp.Payroll.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Tests
{
    [TestFixture, Order(2)]
    public class PayGradeRepositoryTests
    {
        private PayGradeRepository _payGradeRepository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Information("------------------------------------------------------------------------------------------");
            Log.Information("Starting PayGradeRepositoryTests.");
            Log.Information("------------------------------------------------------------------------------------------\r\n");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Log.Information("------------------------------------------------------------------------------------------");
            Log.Information("Finishing PayGradeRepositoryTests.");
            Log.Information("------------------------------------------------------------------------------------------\r\n");
        }

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            PayrollDALDependencyInjection.AddPayrollDALDI(services, Configuration);
            var serviceProvider = services.BuildServiceProvider();
            LTAPayrollDbContext LtaPayrollDbContext = (LTAPayrollDbContext)serviceProvider.GetServices(typeof(LTAPayrollDbContext)).First();

            var _logger = new SerilogLoggerFactory(Log.Logger).CreateLogger<PayGradeRepository>();
    
            _payGradeRepository = new PayGradeRepository(_logger, LtaPayrollDbContext);
        }

        [Test, Order(1)]
        public void GetAllPayGradesAsync()
        {
            Log.Information("Starting GetAllPayGradesAsync - Test.");

            var payGrades = Task.Run(() => _payGradeRepository.GetAllAsync()).Result;
            try
            {
                Assert.That(payGrades, Has.Count.EqualTo(5));
                Log.Information("PayGradeRepository Test - GetAllPayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - GetAllPayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(2)]
        public void GetAllActivePayGradesAsync()
        {
            Log.Information("Starting GetAllActivePayGradesAsync - Test.");
            var payGrades = Task.Run(() => _payGradeRepository.GetFilteredAsync(x => x.IsActive)).Result;
            try
            {
                Assert.That(payGrades, Has.Count.EqualTo(4));
                Log.Information("PayGradeRepository Test - GetAllActivePayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - GetAllActivePayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(3)]
        public void GetByIdActiveExistingItemAsync()
        {
            Log.Information("Starting GetByIdActiveExistingItemAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 1)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(1));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Meister"));
                    Assert.That(payGrade.IsActive, Is.True);
                });
                Log.Information("PayGradeRepository Test - GetByIdActiveExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - GetByIdActiveExistingItemAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(4)]
        public void GetInactiveItemFilterForActiveAsync()
        {
            Log.Information("Starting GetByIdInactiveExistingItemFilterForActiveAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 5 && x.IsActive)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Null);
                });
                Log.Information("PayGradeRepository Test - GetByIdInactiveExistingItemFilterForActiveAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - GetByIdInactiveExistingItemFilterForActiveAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(5)]
        public void GetByIdInactiveExistingItemNoFilterAsync()
        {
            Log.Information("Starting GetByIdInactiveExistingItemNoFilterAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 5)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Not.Null);
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Hilfsarbeiter"));
                    Assert.That(payGrade.IsActive, Is.False);
                });
                Log.Information("PayGradeRepository Test - GetByIdInactiveExistingItemNoFilterAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - GetByIdInactiveExistingItemNoFilterAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(6)]
        public void GetByDeletedIdAsync()
        {
            Log.Information("Starting GetByDeletedIdAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 6)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Null);
                });
                Log.Information("PayGradeRepository Test - GetByDeletedIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - GetByDeletedIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(7)]
        public void CreateNewPayGradeWithDefaultDataAsync()
        {
            Log.Information("Starting CreateNewPayGradeWithDefaultDataAsync - Test.");

            var payGrade = new PayGrade { PayGradeName = "Neue Gehaltsklasse" };
            var createdPayGrade = Task.Run(() => _payGradeRepository.AddAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(createdPayGrade.PayGradeId, Is.EqualTo(7));
                    Assert.That(createdPayGrade.PayGradeName, Is.EqualTo("Neue Gehaltsklasse"));
                    Assert.That(createdPayGrade.IsActive, Is.True);
                });
                Log.Information("PayGradeRepository Test - CreateNewPayGradeWithDefaultDataAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - CreateNewPayGradeWithDefaultDataAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(8)]
        public void CreateNewPayGradeWithEnrichedDataAsync()
        {
            Log.Information("Starting CreateNewPayGradeWithEnrichedDataAsync - Test.");

            var payGrade = new PayGrade { PayGradeName = "Inaktive Gehaltsklasse", IsActive = false };

            var createdPayGrade = Task.Run(() => _payGradeRepository.AddAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(createdPayGrade.PayGradeId, Is.EqualTo(8));
                    Assert.That(createdPayGrade.PayGradeName, Is.EqualTo("Inaktive Gehaltsklasse"));
                    Assert.That(createdPayGrade.IsActive, Is.False);
                });

                Log.Information("PayGradeRepository Test - CreateNewPayGradeWithEnrichedDataAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - CreateNewPayGradeWithEnrichedDataAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(9)]
        public void UpdatePayGradeWithExistingIdAsync()
        {
            Log.Information("Starting UpdatePayGradeWithExistingIdAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 7)).Result;
            payGrade.PayGradeName = "Neue Gehaltsklasse (deaktiviert)";
            payGrade.IsActive = false;
            var updatedPayGrade = Task.Run(() => _payGradeRepository.UpdateAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(updatedPayGrade.PayGradeId, Is.EqualTo(7));
                    Assert.That(updatedPayGrade.PayGradeName, Is.EqualTo("Neue Gehaltsklasse (deaktiviert)"));
                    Assert.That(updatedPayGrade.IsActive, Is.False);
                });

                Log.Information("PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(10)]
        public void UpdateInactivePayGradeAsync()
        {
            Log.Information("Starting UpdateInactivePayGradeAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 5)).Result;
            payGrade.IsActive = true;
            var updatedPayGrade = Task.Run(() => _payGradeRepository.UpdateAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(updatedPayGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(updatedPayGrade.PayGradeName, Is.EqualTo("Hilfsarbeiter"));
                    Assert.That(updatedPayGrade.IsActive, Is.True);
                });

                Log.Information("PayGradeRepository Test - UpdateInactivePayGradeAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - UpdateInactivePayGradeAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(11)]
        public void DeletePayGradeWithExistingIdAsync()
        {
            Log.Information("Starting UpdatePayGradeWithExistingIdAsync - Test.");

            var payGradeCountBefore = Task.Run(() => _payGradeRepository.GetAllAsync()).Result.Count;

            var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 4)).Result;
            var isDeleted = Task.Run(() => _payGradeRepository.DeleteAsync(payGrade)).Result;

            var payGradeCountAfter = Task.Run(() => _payGradeRepository.GetAllAsync()).Result.Count;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(isDeleted, Is.True);
                    Assert.That(payGradeCountAfter, Is.EqualTo(payGradeCountBefore - 1));
                });

                Log.Information("PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished with error. \r\n");
            }
        }

    }
}