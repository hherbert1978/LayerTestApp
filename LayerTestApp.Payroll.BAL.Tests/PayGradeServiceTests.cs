using LayerTestApp.Common.Exceptions;
using LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs;
using LayerTestApp.Payroll.DAL.Entities;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.BAL.Tests
{
    [TestFixture, Order(1)]
    public class PayGradeServiceTests : BaseTestClass
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Logger.LogInformation("------------------------------------------------------------------------------------------");
            Logger.LogInformation("Starting PayGradeServiceTests.");
            Logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Logger.LogInformation("------------------------------------------------------------------------------------------");
            Logger.LogInformation("Finishing PayGradeServiceTests.");
            Logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        #region "GetTests"

        [Test, Order(1)]
        public void GetAllPayGradesAsync()
        {
            Logger.LogInformation("Starting GetAllPayGradesAsync - Test.");

            try
            {
                var payGrades = Task.Run(() => PayGradeService.GetAllAsync()).Result;

                Assert.That(payGrades.Count(), Is.EqualTo(5));
                Logger.LogInformation("PayGradeService Test - GetAllPayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - GetAllPayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(2)]
        public void GetAllActivePayGradesAsync()
        {
            Logger.LogInformation("Starting GetAllActivePayGradesAsync - Test.");
                       
            try
            {
                var activePayGrades = Task.Run(() => PayGradeService.GetAllActiveAsync()).Result;

                Assert.That(activePayGrades.Count(), Is.EqualTo(4));
                Logger.LogInformation("PayGradeService Test - GetAllActivePayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - GetAllActivePayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(3)]
        public void GetAllDeletedPayGradesAsync()
        {
            Logger.LogInformation("Starting GetAllDeletedPayGradesAsync - Test.");

            try
            {
                var deletedPayGrades = Task.Run(() => PayGradeService.GetAllDeletedAsync()).Result;

                Assert.That(deletedPayGrades.Count(), Is.EqualTo(1));
                Logger.LogInformation("PayGradeService Test - GetAllDeletedPayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - GetAllDeletedPayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(4)]
        public void GetPayGradeByIdAsync()
        {
            Logger.LogInformation("Starting GetPayGradeByIdAsync - Test.");

            try
            {
                var payGrade = Task.Run(() => PayGradeService.GetByIdAsync(2)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(2));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Geselle"));
                    Assert.That(payGrade.IsActive, Is.True);
                });

                Logger.LogInformation("PayGradeService Test - GetPayGradeByIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - GetPayGradeByIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(5)]
        public void GetPayGradeByIdNotExistingAsync()
        {
            Logger.LogInformation("Starting GetPayGradeByIdNotExistingAsync - Test.");
                  
            try
            {
                var expectedException = Assert.ThrowsAsync<ResourceNotFoundException>(() => Task.Run(() => PayGradeService.GetByIdAsync(19)));

                Assert.That(expectedException.Message, Is.EqualTo("No Pay Grade with id = \"19\" was found."));
                Logger.LogInformation("PayGradeService Test - GetPayGradeByIdNotExistingAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - GetPayGradeByIdNotExistingAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(6)]
        public void NameExistsWithExistingItemAsync()
        {
            Logger.LogInformation("Starting NameExistsWithExistingItemAsync - Test.");

            try
            {
                var isExisting = Task.Run(() => PayGradeService.NameExistsAsync("Lehrling")).Result;

                Assert.That(isExisting, Is.EqualTo(true));
                Logger.LogInformation("PayGradeService Test - NameExistsWithExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - NameExistsWithExistingItemAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(7)]
        public void NameExistsWithNotExistingItemAsync()
        {
            Logger.LogInformation("Starting NameExistsWithNotExistingItemAsync - Test.");

            try
            {
                var isExisting = Task.Run(() => PayGradeService.NameExistsAsync("Aushilfe")).Result;

                Assert.That(isExisting, Is.EqualTo(false));
                Logger.LogInformation("PayGradeService Test - NameExistsWithNotExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - NameExistsWithNotExistingItemAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(8)]
        public void GetByNameAsyncWithExistingItemAsync()
        {
            Logger.LogInformation("Starting GetByNameAsyncWithExistingItemAsync - Test.");

            try
            {
                var payGrade = Task.Run(() => PayGradeService.GetByNameAsync("Lehrling")).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(3));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Lehrling"));
                    Assert.That(payGrade.IsActive, Is.True);
                });

                Logger.LogInformation("PayGradeService Test - GetByNameAsyncWithExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - GetByNameAsyncWithExistingItemAsync - Test finished with error. \r\n");
            }

        }

        [Test, Order(9)]
        public void GetByNameWithNotExistingItemAsync()
        {
            Logger.LogInformation("Starting GetByNameWithNotExistingItemAsync - Test.");

            try
            {
                var expectedException = Assert.ThrowsAsync<ResourceNotFoundException>(() => Task.Run(() => PayGradeService.GetByNameAsync("Aushilfe")));

                Assert.That(expectedException.Message, Is.EqualTo("No Pay Grade with name = \"Aushilfe\" was found."));
                Logger.LogInformation("PayGradeService Test - GetByNameWithNotExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - GetByNameWithNotExistingItemAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(10)]
        public void GetIdByNameAsyncWithExistingItemAsync()
        {
            Logger.LogInformation("Starting GetIdByNameAsyncWithExistingItemAsync - Test.");

            try
            {
                var id = Task.Run(() => PayGradeService.GetIdByNameAsync("Lehrling")).Result;

                Assert.That(id, Is.EqualTo(3));
                Logger.LogInformation("PayGradeService Test - GetIdByNameAsyncWithExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - GetIdByNameAsyncWithExistingItemAsync - Test finished with error. \r\n");
            }

        }

        [Test, Order(11)]
        public void GetIdByNameWithNotExistingItemAsync()
        {
            Logger.LogInformation("Starting GetIdByNameWithNotExistingItemAsync - Test.");

            try
            {
                var expectedException = Assert.ThrowsAsync<ResourceNotFoundException>(() => Task.Run(() => PayGradeService.GetIdByNameAsync("Aushilfe")));

                Assert.That(expectedException.Message, Is.EqualTo("No Pay Grade with name = \"Aushilfe\" was found."));
                Logger.LogInformation("PayGradeService Test - GetIdByNameWithNotExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - GetIdByNameWithNotExistingItemAsync - Test finished with error. \r\n");
            }
        }

        #endregion

        #region "Create, Update, Delete Tests"

        [Test, Order(12)]
        public void CreatePayGradeAsync()
        {
            Logger.LogInformation("Starting CreatePayGradeAsync - Test.");
            var payGradeDTO = new CreatePayGradeDTO("Neue Gehaltsstufe");
           
            try
            {
                var payGrade = Task.Run(() => PayGradeService.CreateAsync(payGradeDTO)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(7));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Neue Gehaltsstufe"));
                    Assert.That(payGrade.IsActive, Is.True);
                });

                Logger.LogInformation("PayGradeService Test - CreatePayGradeAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - CreatePayGradeAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(13)]
        public void UpdatePayGradeChangeNameAsync()
        {
            Logger.LogInformation("Starting UpdatePayGradeChangeNameAsync - Test.");
            var payGradeDTO = new UpdatePayGradeDTO(5, "Bearbeiteter Hilfsarbeiter", false);

            try
            {
                var payGrade = Task.Run(() => PayGradeService.UpdateAsync(payGradeDTO)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Bearbeiteter Hilfsarbeiter"));
                    Assert.That(payGrade.IsActive, Is.False);
                });

                Logger.LogInformation("PayGradeService Test - UpdatePayGradeChangeNameAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - UpdatePayGradeChangeNameAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(14)]
        public void UpdatePayGradeChangeActiveStatusAsync()
        {
            Logger.LogInformation("Starting UpdatePayGradeChangeActiveStatusAsync - Test.");
            var payGradeDTO = new UpdatePayGradeDTO(5, "Bearbeiteter Hilfsarbeiter", true);

            try
            {
                var payGrade = Task.Run(() => PayGradeService.UpdateAsync(payGradeDTO)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Bearbeiteter Hilfsarbeiter"));
                    Assert.That(payGrade.IsActive, Is.True);
                });

                Logger.LogInformation("PayGradeService Test - UpdatePayGradeChangeActiveStatusAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - UpdatePayGradeChangeActiveStatusAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(15)]
        public void UpdatePayGradeNotExistingItemAsync()
        {
            Logger.LogInformation("Starting UpdatePayGradeNotExistingItemAsync - Test.");
            var payGradeDTO = new UpdatePayGradeDTO(99, "Bearbeiteter Hilfsarbeiter", true);

            try
            {
                var expectedException = Assert.ThrowsAsync<ResourceNotFoundException>(() => Task.Run(() => PayGradeService.UpdateAsync(payGradeDTO)));
                Assert.That(expectedException.Message, Is.EqualTo("No Pay Grade with id = \"99\" was found."));

                Logger.LogInformation("PayGradeService Test - UpdatePayGradeNotExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - UpdatePayGradeNotExistingItemAsync - Test finished with error. \r\n");
            }
        }
                
        [Test, Order(16)]
        public void DeletePayGradeAsync()
        {
            Logger.LogInformation("Starting DeletePayGradeAsync - Test.");
            var payGradeDTO = new DeletePayGradeDTO(5);

            try
            {
                var payGrade = Task.Run(() => PayGradeService.DeleteAsync(payGradeDTO)).Result;

                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(payGrade.IsDeleted, Is.True);
                });

                Logger.LogInformation("PayGradeService Test - DeletePayGradeAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - DeletePayGradeAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(17)]
        public void DeletePayGradeNotExistingItemAsync()
        {
            Logger.LogInformation("Starting DeletePayGradeNotExistingItemAsync - Test.");
            var payGradeDTO = new DeletePayGradeDTO(99);

            try
            {
                var expectedException = Assert.ThrowsAsync<ResourceNotFoundException>(() => Task.Run(() => PayGradeService.DeleteAsync(payGradeDTO)));
                Assert.That(expectedException.Message, Is.EqualTo("No Pay Grade with id = \"99\" was found."));

                Logger.LogInformation("PayGradeService Test - DeletePayGradeNotExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeService Test - DeletePayGradeNotExistingItemAsync - Test finished with error. \r\n");
            }
        }

        #endregion
    }
}