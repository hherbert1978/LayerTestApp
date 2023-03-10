using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Models;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Tests
{  
    [TestFixture, Order(1)]
    public class ContextTests : BaseTestClass
    {

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Logger.LogInformation("------------------------------------------------------------------------------------------");
            Logger.LogInformation("Starting ContextTests.");
            Logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Logger.LogInformation("------------------------------------------------------------------------------------------");
            Logger.LogInformation("Finishing ContextTests.");
            Logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void GetAllPayGrades()
        {
            Logger.LogInformation("Starting GetAllPayGrades - Test.");
            IEnumerable<PayGrade> payGrades = LtaPayrollDbContext.PayGrades.ToList();
            try
            {
                Assert.That(payGrades.Count(), Is.EqualTo(5));
                Logger.LogInformation("GetAllPayGrades - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "GetAllPayGrades - Test finished with error. \r\n");
            }
        }

        [Test, Order(2)]
        public void GetAllActivePayGrades()
        {
            Logger.LogInformation("Starting GetAllActivePayGrades - Test.");
            IEnumerable<PayGrade> payGrades = LtaPayrollDbContext.PayGrades.Where(x => x.IsActive).ToList();
            try
            {
                Assert.That(payGrades.Count(), Is.EqualTo(4));
                Logger.LogInformation("GetAllActivePayGrades - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Logger.LogInformation(ex, "GetAllActivePayGrades - Test finished with error. \r\n");
            }
        }
    }
}