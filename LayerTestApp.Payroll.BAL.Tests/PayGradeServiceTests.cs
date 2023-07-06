using LayerTestApp.Common.Exceptions;
using LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.BAL.Tests
{
    [TestFixture, Order(1)]
    public class PayGradeServiceTests : BaseTestClass
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger.LogInformation("------------------------------------------------------------------------------------------");
            _logger.LogInformation("Starting PayGradeServiceTests.");
            _logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _logger.LogInformation("------------------------------------------------------------------------------------------");
            _logger.LogInformation("Finishing PayGradeServiceTests.");
            _logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        #region "GetTests"

        [Test, Order(1)]
        public void GetAllPayGradesAsync()
        {
            _logger.LogInformation("Starting GetAllPayGradesAsync - Test.");

            try
            {
                var payGrades = Task.Run(() => _payGradeService.GetAllAsync()).Result;

                Assert.That(payGrades.Count(), Is.EqualTo(5));
                _logger.LogInformation("PayGradeService Test - GetAllPayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - GetAllPayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(2)]
        public void GetAllActivePayGradesAsync()
        {
            _logger.LogInformation("Starting GetAllActivePayGradesAsync - Test.");

            try
            {
                var activePayGrades = Task.Run(() => _payGradeService.GetAllActiveAsync()).Result;

                Assert.That(activePayGrades.Count(), Is.EqualTo(4));
                _logger.LogInformation("PayGradeService Test - GetAllActivePayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - GetAllActivePayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(3)]
        public void GetAllDeletedPayGradesAsync()
        {
            _logger.LogInformation("Starting GetAllDeletedPayGradesAsync - Test.");

            try
            {
                var deletedPayGrades = Task.Run(() => _payGradeService.GetAllDeletedAsync()).Result;

                Assert.That(deletedPayGrades.Count(), Is.EqualTo(1));
                _logger.LogInformation("PayGradeService Test - GetAllDeletedPayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - GetAllDeletedPayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(4)]
        public void GetPayGradeByIdAsync()
        {
            _logger.LogInformation("Starting GetPayGradeByIdAsync - Test.");

            try
            {
                var payGrade = Task.Run(() => _payGradeService.GetByIdAsync(2)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(2));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Geselle"));
                    Assert.That(payGrade.IsActive, Is.True);
                });

                _logger.LogInformation("PayGradeService Test - GetPayGradeByIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - GetPayGradeByIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(5)]
        public void GetPayGradeByIdNotExistingAsync()
        {
            _logger.LogInformation("Starting GetPayGradeByIdNotExistingAsync - Test.");

            try
            {
                var expectedException = Assert.ThrowsAsync<ResourceNotFoundException>(() => Task.Run(() => _payGradeService.GetByIdAsync(19)));

                Assert.That(expectedException.Message, Is.EqualTo("No Pay Grade with id = '19' was found."));
                _logger.LogInformation("PayGradeService Test - GetPayGradeByIdNotExistingAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - GetPayGradeByIdNotExistingAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(6)]
        public void NameExistsWithExistingItemAsync()
        {
            _logger.LogInformation("Starting NameExistsWithExistingItemAsync - Test.");

            try
            {
                var isExisting = Task.Run(() => _payGradeService.NameExistsAsync("Lehrling")).Result;

                Assert.That(isExisting, Is.EqualTo(true));
                _logger.LogInformation("PayGradeService Test - NameExistsWithExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - NameExistsWithExistingItemAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(7)]
        public void NameExistsWithNotExistingItemAsync()
        {
            _logger.LogInformation("Starting NameExistsWithNotExistingItemAsync - Test.");

            try
            {
                var isExisting = Task.Run(() => _payGradeService.NameExistsAsync("Aushilfe")).Result;

                Assert.That(isExisting, Is.EqualTo(false));
                _logger.LogInformation("PayGradeService Test - NameExistsWithNotExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - NameExistsWithNotExistingItemAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(8)]
        public void GetByNameAsyncWithExistingItemAsync()
        {
            _logger.LogInformation("Starting GetByNameAsyncWithExistingItemAsync - Test.");

            try
            {
                var payGrade = Task.Run(() => _payGradeService.GetByNameAsync("Lehrling")).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(3));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Lehrling"));
                    Assert.That(payGrade.IsActive, Is.True);
                });

                _logger.LogInformation("PayGradeService Test - GetByNameAsyncWithExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - GetByNameAsyncWithExistingItemAsync - Test finished with error. \r\n");
            }

        }

        [Test, Order(9)]
        public void GetByNameWithNotExistingItemAsync()
        {
            _logger.LogInformation("Starting GetByNameWithNotExistingItemAsync - Test.");

            try
            {
                var expectedException = Assert.ThrowsAsync<ResourceNotFoundException>(() => Task.Run(() => _payGradeService.GetByNameAsync("Aushilfe")));

                Assert.That(expectedException.Message, Is.EqualTo("No Pay Grade with name = 'Aushilfe' was found."));
                _logger.LogInformation("PayGradeService Test - GetByNameWithNotExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - GetByNameWithNotExistingItemAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(10)]
        public void GetIdByNameAsyncWithExistingItemAsync()
        {
            _logger.LogInformation("Starting GetIdByNameAsyncWithExistingItemAsync - Test.");

            try
            {
                var id = Task.Run(() => _payGradeService.GetIdByNameAsync("Lehrling")).Result;

                Assert.That(id, Is.EqualTo(3));
                _logger.LogInformation("PayGradeService Test - GetIdByNameAsyncWithExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - GetIdByNameAsyncWithExistingItemAsync - Test finished with error. \r\n");
            }

        }

        [Test, Order(11)]
        public void GetIdByNameWithNotExistingItemAsync()
        {
            _logger.LogInformation("Starting GetIdByNameWithNotExistingItemAsync - Test.");

            try
            {
                var expectedException = Assert.ThrowsAsync<ResourceNotFoundException>(() => Task.Run(() => _payGradeService.GetIdByNameAsync("Aushilfe")));

                Assert.That(expectedException.Message, Is.EqualTo("No Pay Grade with name = 'Aushilfe' was found."));
                _logger.LogInformation("PayGradeService Test - GetIdByNameWithNotExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - GetIdByNameWithNotExistingItemAsync - Test finished with error. \r\n");
            }
        }

        #endregion

        #region "Create, Update, Delete Tests"

        [Test, Order(12)]
        public void CreatePayGradeAsync()
        {
            _logger.LogInformation("Starting CreatePayGradeAsync - Test.");
            var payGradeDTO = new CreatePayGradeDTO("Neue Gehaltsstufe");

            try
            {
                var payGrade = Task.Run(() => _payGradeService.CreateAsync(payGradeDTO)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(7));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Neue Gehaltsstufe"));
                    Assert.That(payGrade.IsActive, Is.True);
                });

                _logger.LogInformation("PayGradeService Test - CreatePayGradeAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - CreatePayGradeAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(13)]
        public void UpdatePayGradeChangeNameAsync()
        {
            _logger.LogInformation("Starting UpdatePayGradeChangeNameAsync - Test.");
            var payGradeDTO = new UpdatePayGradeDTO(5, "Bearbeiteter Hilfsarbeiter", false);

            try
            {
                var payGrade = Task.Run(() => _payGradeService.UpdateAsync(payGradeDTO)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Bearbeiteter Hilfsarbeiter"));
                    Assert.That(payGrade.IsActive, Is.False);
                });

                _logger.LogInformation("PayGradeService Test - UpdatePayGradeChangeNameAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - UpdatePayGradeChangeNameAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(14)]
        public void UpdatePayGradeChangeActiveStatusAsync()
        {
            _logger.LogInformation("Starting UpdatePayGradeChangeActiveStatusAsync - Test.");
            var payGradeDTO = new UpdatePayGradeDTO(5, "Bearbeiteter Hilfsarbeiter", true);

            try
            {
                var payGrade = Task.Run(() => _payGradeService.UpdateAsync(payGradeDTO)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Bearbeiteter Hilfsarbeiter"));
                    Assert.That(payGrade.IsActive, Is.True);
                });

                _logger.LogInformation("PayGradeService Test - UpdatePayGradeChangeActiveStatusAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - UpdatePayGradeChangeActiveStatusAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(15)]
        public void UpdatePayGradeNotExistingItemAsync()
        {
            _logger.LogInformation("Starting UpdatePayGradeNotExistingItemAsync - Test.");
            var payGradeDTO = new UpdatePayGradeDTO(99, "Bearbeiteter Hilfsarbeiter", true);

            try
            {
                var expectedException = Assert.ThrowsAsync<ResourceNotFoundException>(() => Task.Run(() => _payGradeService.UpdateAsync(payGradeDTO)));
                Assert.That(expectedException.Message, Is.EqualTo("No Pay Grade with id = '99' was found."));

                _logger.LogInformation("PayGradeService Test - UpdatePayGradeNotExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - UpdatePayGradeNotExistingItemAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(16)]
        public void DeletePayGradeAsync()
        {
            _logger.LogInformation("Starting DeletePayGradeAsync - Test.");
            var payGradeDTO = new DeletePayGradeDTO(5);

            try
            {
                var payGrade = Task.Run(() => _payGradeService.DeleteAsync(payGradeDTO)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(payGrade.IsDeleted, Is.True);
                });

                _logger.LogInformation("PayGradeService Test - DeletePayGradeAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - DeletePayGradeAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(17)]
        public void DeletePayGradeNotExistingItemAsync()
        {
            _logger.LogInformation("Starting DeletePayGradeNotExistingItemAsync - Test.");
            var payGradeDTO = new DeletePayGradeDTO(99);

            try
            {
                var expectedException = Assert.ThrowsAsync<ResourceNotFoundException>(() => Task.Run(() => _payGradeService.DeleteAsync(payGradeDTO)));
                Assert.That(expectedException.Message, Is.EqualTo("No Pay Grade with id = '99' was found."));

                _logger.LogInformation("PayGradeService Test - DeletePayGradeNotExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "PayGradeService Test - DeletePayGradeNotExistingItemAsync - Test finished with error. \r\n");
            }
        }

        #endregion
    }
}