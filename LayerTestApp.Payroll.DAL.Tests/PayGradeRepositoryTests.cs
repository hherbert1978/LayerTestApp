using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Entities;
using LayerTestApp.Payroll.DAL.Repositories;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Tests
{
    [TestFixture, Order(2)]
    public class PayGradeRepositoryTests
    {
        private LTAPayrollDbContext _ltaPayrollDbContext;
        private ILogger<PayGradeRepository> _logger;
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
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - GetAllPayGradesAsync - Test finished with error. \r\n");
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
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - GetAllActivePayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(3)]
        public void GetByIdActiveExistingItemAsync()
        {
            Log.Information("Starting GetByIdActiveExistingItemAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetByIdAsync(1)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.Not.Null);
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
        public void GetByIdInactiveExistingItemFilterForActiveAsync()
        {
            Log.Information("Starting GetByIdInactiveExistingItemFilterForActiveAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetByIdAsync(5)).Result;

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
            var payGrade = Task.Run(() => _payGradeRepository.GetByIdAsync(5, false)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Not.Null);
                    Assert.That(payGrade.PayGradeId, Is.Not.Null);
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
        public void GetByIdDeletedIdAsync()
        {
            Log.Information("Starting GetByIdDeletedIdAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetByIdAsync(6, false)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Null);
                });
                Log.Information("PayGradeRepository Test - GetByIdDeletedIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - GetByIdDeletedIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(7)]
        public void CreateNewPayGradeWithDefaultDataAsync()
        {
            Log.Information("Starting CreateNewPayGradeWithDefaultDataAsync - Test.");

            var payGrade = new PayGrade("Neue Gehaltsklasse");
            var createdPayGrade = Task.Run(() => _payGradeRepository.AddAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(createdPayGrade.PayGradeId, Is.Not.Null);
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

            var payGrade = new PayGrade("Inaktive Gehaltsklasse", false);
            var createdPayGrade = Task.Run(() => _payGradeRepository.AddAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(createdPayGrade.PayGradeId, Is.Not.Null);
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
        public void UpdatePayGradeWithNotExistingIdAsync()
        {
            Log.Information("Starting UpdatePayGradeWithNotExistingIdAsync - Test.");
            var payGrade = new PayGrade(11, "NotExistingPayGrade", false);

            try
            {
                Assert.ThrowsAsync(Is.TypeOf<KeyNotFoundException>()
                    .And.Message.EqualTo("No PayGrade with id \"11\" exists."),
                    () => Task.Run(() => _payGradeRepository.UpdateAsync(payGrade))
                );
                Log.Information("PayGradeRepository Test - UpdatePayGradeWithNotExistingIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - UpdatePayGradeWithNotExistingIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(10)]
        public void UpdatePayGradeWithExistingIdAsync()
        {
            Log.Information("Starting UpdatePayGradeWithExistingIdAsync - Test.");
            var payGrade = new PayGrade(7, "Neue Gehaltsklasse (deaktiviert)", false);
            var updatedPayGrade = Task.Run(() => _payGradeRepository.UpdateAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(updatedPayGrade.PayGradeId, Is.Not.Null);
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
            var payGrade = new PayGrade(5, true);
            var updatedPayGrade = Task.Run(() => _payGradeRepository.UpdateAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(updatedPayGrade.PayGradeId, Is.Not.Null);
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

        [Test, Order(12)]
        public void DeletePayGradeWithNotExistingIdAsync()
        {
            Log.Information("Starting DeletePayGradeWithNotExistingIdAsync - Test.");
            var payGrade = new PayGrade(11, "NotExistingPayGrade", true);

            try
            {
                Assert.ThrowsAsync(Is.TypeOf<KeyNotFoundException>()
                    .And.Message.EqualTo("No PayGrade with id \"11\" exists."),
                    () => Task.Run(() => _payGradeRepository.DeleteAsync(payGrade))
                );
                Log.Information("PayGradeRepository Test - DeletePayGradeWithNotExistingIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "PayGradeRepository Test - DeletePayGradeWithNotExistingIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(13)]
        public void DeletePayGradeWithExistingIdAsync()
        {
            Log.Information("Starting UpdatePayGradeWithExistingIdAsync - Test.");

            var payGradeCountBefore = Task.Run(() => _payGradeRepository.GetAllAsync(false)).Result.Count;

            var payGrade = new PayGrade(4);
            var isDeleted = Task.Run(() => _payGradeRepository.DeleteAsync(payGrade)).Result;

            var payGradeCountAfter = Task.Run(() => _payGradeRepository.GetAllAsync(false)).Result.Count;

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