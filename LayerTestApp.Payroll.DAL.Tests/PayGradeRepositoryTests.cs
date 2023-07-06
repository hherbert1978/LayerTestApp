using LayerTestApp.Payroll.DAL.Entities;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Tests
{
    [TestFixture, Order(2)]
    public class PayGradeRepositoryTests : BaseTestClass
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger.LogInformation("------------------------------------------------------------------------------------------");
            _logger.LogInformation("Starting PayGradeRepositoryTests.");
            _logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _logger.LogInformation("------------------------------------------------------------------------------------------");
            _logger.LogInformation("Finishing PayGradeRepositoryTests.");
            _logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void GetAllPayGradesAsync()
        {
            _logger.LogInformation("Starting GetAllPayGradesAsync - Test.");

            try
            {
                var payGrades = Task.Run(() => _payGradeRepository.GetAllAsync()).Result;

                Assert.That(payGrades.Count(), Is.EqualTo(5));
                _logger.LogInformation("PayGradeRepository Test - GetAllPayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - GetAllPayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(2)]
        public void GetAllActivePayGradesAsync()
        {
            _logger.LogInformation("Starting GetAllActivePayGradesAsync - Test.");

            try
            {
                var payGrades = Task.Run(() => _payGradeRepository.GetFilteredAsync(x => x.IsActive)).Result;

                Assert.That(payGrades.Count(), Is.EqualTo(4));
                _logger.LogInformation("PayGradeRepository Test - GetAllActivePayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - GetAllActivePayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(3)]
        public void GetAllDeletedPayGradesAsync()
        {
            _logger.LogInformation("Starting GetAllDeletedPayGradesAsync - Test.");

            try
            {
                var payGrades = Task.Run(() => _payGradeRepository.GetAllDeletedAsync()).Result;

                Assert.That(payGrades.Count(), Is.EqualTo(1));
                _logger.LogInformation("PayGradeRepository Test - GetAllDeletedPayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - GetAllDeletedPayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(4)]
        public void GetByIdActiveExistingItemAsync()
        {
            _logger.LogInformation("Starting GetByIdActiveExistingItemAsync - Test.");

            try
            {
                var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 1)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(1));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Meister"));
                    Assert.That(payGrade.IsActive, Is.True);
                });
                _logger.LogInformation("PayGradeRepository Test - GetByIdActiveExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - GetByIdActiveExistingItemAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(5)]
        public void GetInactiveItemFilterForActiveAsync()
        {
            _logger.LogInformation("Starting GetByIdInactiveExistingItemFilterForActiveAsync - Test.");

            try
            {
                var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 5 && x.IsActive)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Null);
                });
                _logger.LogInformation("PayGradeRepository Test - GetByIdInactiveExistingItemFilterForActiveAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - GetByIdInactiveExistingItemFilterForActiveAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(6)]
        public void GetByIdInactiveExistingItemNoFilterAsync()
        {
            _logger.LogInformation("Starting GetByIdInactiveExistingItemNoFilterAsync - Test.");

            try
            {
                var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 5)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Not.Null);
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Hilfsarbeiter"));
                    Assert.That(payGrade.IsActive, Is.False);
                });
                _logger.LogInformation("PayGradeRepository Test - GetByIdInactiveExistingItemNoFilterAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - GetByIdInactiveExistingItemNoFilterAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(7)]
        public void GetByDeletedIdAsync()
        {
            _logger.LogInformation("Starting GetByDeletedIdAsync - Test.");

            try
            {
                var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 6)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Null);
                });
                _logger.LogInformation("PayGradeRepository Test - GetByDeletedIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "ayGradeRepository Test - GetByDeletedIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(8)]
        public void CreateNewPayGradeWithDefaultDataAsync()
        {
            _logger.LogInformation("Starting CreateNewPayGradeWithDefaultDataAsync - Test.");

            var payGrade = new PayGrade { PayGradeName = "Neue Gehaltsklasse" };

            try
            {
                var createdPayGrade = Task.Run(() => _payGradeRepository.AddAsync(payGrade)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(createdPayGrade.PayGradeId, Is.EqualTo(7));
                    Assert.That(createdPayGrade.PayGradeName, Is.EqualTo("Neue Gehaltsklasse"));
                    Assert.That(createdPayGrade.IsActive, Is.True);
                });
                _logger.LogInformation("PayGradeRepository Test - CreateNewPayGradeWithDefaultDataAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - CreateNewPayGradeWithDefaultDataAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(9)]
        public void CreateNewPayGradeWithEnrichedDataAsync()
        {
            _logger.LogInformation("Starting CreateNewPayGradeWithEnrichedDataAsync - Test.");

            var payGrade = new PayGrade { PayGradeName = "Inaktive Gehaltsklasse", IsActive = false };

            try
            {
                var createdPayGrade = Task.Run(() => _payGradeRepository.AddAsync(payGrade)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(createdPayGrade.PayGradeId, Is.EqualTo(8));
                    Assert.That(createdPayGrade.PayGradeName, Is.EqualTo("Inaktive Gehaltsklasse"));
                    Assert.That(createdPayGrade.IsActive, Is.False);
                });

                _logger.LogInformation("PayGradeRepository Test - CreateNewPayGradeWithEnrichedDataAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - CreateNewPayGradeWithEnrichedDataAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(10)]
        public void UpdatePayGradeWithExistingIdAsync()
        {
            _logger.LogInformation("Starting UpdatePayGradeWithExistingIdAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 7)).Result;
            payGrade.PayGradeName = "Neue Gehaltsklasse (deaktiviert)";
            payGrade.IsActive = false;

            try
            {
                var updatedPayGrade = Task.Run(() => _payGradeRepository.UpdateAsync(payGrade)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(updatedPayGrade.PayGradeId, Is.EqualTo(7));
                    Assert.That(updatedPayGrade.PayGradeName, Is.EqualTo("Neue Gehaltsklasse (deaktiviert)"));
                    Assert.That(updatedPayGrade.IsActive, Is.False);
                });

                _logger.LogInformation("PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(11)]
        public void UpdateInactivePayGradeAsync()
        {
            _logger.LogInformation("Starting UpdateInactivePayGradeAsync - Test.");
            var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 5)).Result;
            payGrade.IsActive = true;

            try
            {
                var updatedPayGrade = Task.Run(() => _payGradeRepository.UpdateAsync(payGrade)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(updatedPayGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(updatedPayGrade.PayGradeName, Is.EqualTo("Hilfsarbeiter"));
                    Assert.That(updatedPayGrade.IsActive, Is.True);
                });

                _logger.LogInformation("PayGradeRepository Test - UpdateInactivePayGradeAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - UpdateInactivePayGradeAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(12)]
        public void DeletePayGradeWithExistingIdAsync()
        {
            _logger.LogInformation("Starting UpdatePayGradeWithExistingIdAsync - Test.");

            var payGradeCountBefore = Task.Run(() => _payGradeRepository.GetAllAsync()).Result.Count();

            var payGrade = Task.Run(() => _payGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 4)).Result;
            var isDeleted = Task.Run(() => _payGradeRepository.DeleteAsync(payGrade)).Result;

            try
            {
                var payGradeCountAfter = Task.Run(() => _payGradeRepository.GetAllAsync()).Result.Count();

                Assert.Multiple(() =>
                {
                    Assert.That(isDeleted, Is.True);
                    Assert.That(payGradeCountAfter, Is.EqualTo(payGradeCountBefore - 1));
                });

                _logger.LogInformation("PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished with error. \r\n");
            }
        }

    }
}