using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Models;
using LayerTestApp.Payroll.DAL.Repositories;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Tests
{
    [TestFixture, Order(2)]
    public class PayGradeRepositoryTests : BaseTestClass
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Logger.LogInformation("------------------------------------------------------------------------------------------");
            Logger.LogInformation("Starting PayGradeRepositoryTests.");
            Logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Logger.LogInformation("------------------------------------------------------------------------------------------");
            Logger.LogInformation("Finishing PayGradeRepositoryTests.");
            Logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void GetAllPayGradesAsync()
        {
            Logger.LogInformation("Starting GetAllPayGradesAsync - Test.");

            var payGrades = Task.Run(() => PayGradeRepository.GetAllAsync()).Result;
            try
            {
                Assert.That(payGrades, Has.Count.EqualTo(5));
                Logger.LogInformation("PayGradeRepository Test - GetAllPayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - GetAllPayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(2)]
        public void GetAllActivePayGradesAsync()
        {
            Logger.LogInformation("Starting GetAllActivePayGradesAsync - Test.");
            var payGrades = Task.Run(() => PayGradeRepository.GetFilteredAsync(x => x.IsActive)).Result;
            try
            {
                Assert.That(payGrades, Has.Count.EqualTo(4));
                Logger.LogInformation("PayGradeRepository Test - GetAllActivePayGradesAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - GetAllActivePayGradesAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(3)]
        public void GetByIdActiveExistingItemAsync()
        {
            Logger.LogInformation("Starting GetByIdActiveExistingItemAsync - Test.");
            var payGrade = Task.Run(() => PayGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 1)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(1));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Meister"));
                    Assert.That(payGrade.IsActive, Is.True);
                });
                Logger.LogInformation("PayGradeRepository Test - GetByIdActiveExistingItemAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - GetByIdActiveExistingItemAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(4)]
        public void GetInactiveItemFilterForActiveAsync()
        {
            Logger.LogInformation("Starting GetByIdInactiveExistingItemFilterForActiveAsync - Test.");
            var payGrade = Task.Run(() => PayGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 5 && x.IsActive)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Null);
                });
                Logger.LogInformation("PayGradeRepository Test - GetByIdInactiveExistingItemFilterForActiveAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - GetByIdInactiveExistingItemFilterForActiveAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(5)]
        public void GetByIdInactiveExistingItemNoFilterAsync()
        {
            Logger.LogInformation("Starting GetByIdInactiveExistingItemNoFilterAsync - Test.");
            var payGrade = Task.Run(() => PayGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 5)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Not.Null);
                    Assert.That(payGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(payGrade.PayGradeName, Is.EqualTo("Hilfsarbeiter"));
                    Assert.That(payGrade.IsActive, Is.False);
                });
                Logger.LogInformation("PayGradeRepository Test - GetByIdInactiveExistingItemNoFilterAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - GetByIdInactiveExistingItemNoFilterAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(6)]
        public void GetByDeletedIdAsync()
        {
            Logger.LogInformation("Starting GetByDeletedIdAsync - Test.");
            var payGrade = Task.Run(() => PayGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 6)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(payGrade, Is.Null);
                });
                Logger.LogInformation("PayGradeRepository Test - GetByDeletedIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - GetByDeletedIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(7)]
        public void CreateNewPayGradeWithDefaultDataAsync()
        {
            Logger.LogInformation("Starting CreateNewPayGradeWithDefaultDataAsync - Test.");

            var payGrade = new PayGrade { PayGradeName = "Neue Gehaltsklasse" };
            var createdPayGrade = Task.Run(() => PayGradeRepository.AddAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(createdPayGrade.PayGradeId, Is.EqualTo(7));
                    Assert.That(createdPayGrade.PayGradeName, Is.EqualTo("Neue Gehaltsklasse"));
                    Assert.That(createdPayGrade.IsActive, Is.True);
                });
                Logger.LogInformation("PayGradeRepository Test - CreateNewPayGradeWithDefaultDataAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - CreateNewPayGradeWithDefaultDataAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(8)]
        public void CreateNewPayGradeWithEnrichedDataAsync()
        {
            Logger.LogInformation("Starting CreateNewPayGradeWithEnrichedDataAsync - Test.");

            var payGrade = new PayGrade { PayGradeName = "Inaktive Gehaltsklasse", IsActive = false };

            var createdPayGrade = Task.Run(() => PayGradeRepository.AddAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(createdPayGrade.PayGradeId, Is.EqualTo(8));
                    Assert.That(createdPayGrade.PayGradeName, Is.EqualTo("Inaktive Gehaltsklasse"));
                    Assert.That(createdPayGrade.IsActive, Is.False);
                });

                Logger.LogInformation("PayGradeRepository Test - CreateNewPayGradeWithEnrichedDataAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - CreateNewPayGradeWithEnrichedDataAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(9)]
        public void UpdatePayGradeWithExistingIdAsync()
        {
            Logger.LogInformation("Starting UpdatePayGradeWithExistingIdAsync - Test.");
            var payGrade = Task.Run(() => PayGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 7)).Result;
            payGrade.PayGradeName = "Neue Gehaltsklasse (deaktiviert)";
            payGrade.IsActive = false;
            var updatedPayGrade = Task.Run(() => PayGradeRepository.UpdateAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(updatedPayGrade.PayGradeId, Is.EqualTo(7));
                    Assert.That(updatedPayGrade.PayGradeName, Is.EqualTo("Neue Gehaltsklasse (deaktiviert)"));
                    Assert.That(updatedPayGrade.IsActive, Is.False);
                });

                Logger.LogInformation("PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(10)]
        public void UpdateInactivePayGradeAsync()
        {
            Logger.LogInformation("Starting UpdateInactivePayGradeAsync - Test.");
            var payGrade = Task.Run(() => PayGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 5)).Result;
            payGrade.IsActive = true;
            var updatedPayGrade = Task.Run(() => PayGradeRepository.UpdateAsync(payGrade)).Result;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(updatedPayGrade.PayGradeId, Is.EqualTo(5));
                    Assert.That(updatedPayGrade.PayGradeName, Is.EqualTo("Hilfsarbeiter"));
                    Assert.That(updatedPayGrade.IsActive, Is.True);
                });

                Logger.LogInformation("PayGradeRepository Test - UpdateInactivePayGradeAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - UpdateInactivePayGradeAsync - Test finished with error. \r\n");
            }
        }

        [Test, Order(11)]
        public void DeletePayGradeWithExistingIdAsync()
        {
            Logger.LogInformation("Starting UpdatePayGradeWithExistingIdAsync - Test.");

            var payGradeCountBefore = Task.Run(() => PayGradeRepository.GetAllAsync()).Result.Count;

            var payGrade = Task.Run(() => PayGradeRepository.GetFirstOrDefaultAsync(x => x.PayGradeId == 4)).Result;
            var isDeleted = Task.Run(() => PayGradeRepository.DeleteAsync(payGrade)).Result;

            var payGradeCountAfter = Task.Run(() => PayGradeRepository.GetAllAsync()).Result.Count;

            try
            {
                Assert.Multiple(() =>
                {
                    Assert.That(isDeleted, Is.True);
                    Assert.That(payGradeCountAfter, Is.EqualTo(payGradeCountBefore - 1));
                });

                Logger.LogInformation("PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "PayGradeRepository Test - UpdatePayGradeWithExistingIdAsync - Test finished with error. \r\n");
            }
        }

    }
}