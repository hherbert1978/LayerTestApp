using LayerTestApp.Payroll.DAL.Entities;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Tests
{
    [TestFixture, Order(1)]
    public class ContextTests : BaseTestClass
    {

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger.LogInformation("------------------------------------------------------------------------------------------");
            _logger.LogInformation("Starting ContextTests.");
            _logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _logger.LogInformation("------------------------------------------------------------------------------------------");
            _logger.LogInformation("Finishing ContextTests.");
            _logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void GetAllPayGrades()
        {
            _logger.LogInformation("Starting GetAllPayGrades - Test.");
            IEnumerable<PayGrade> payGrades = _context.PayGrades.ToList();
            try
            {
                Assert.That(payGrades.Count(), Is.EqualTo(5));
                _logger.LogInformation("GetAllPayGrades - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "GetAllPayGrades - Test finished with error. \r\n");
            }
        }

        [Test, Order(2)]
        public void GetAllActivePayGrades()
        {
            _logger.LogInformation("Starting GetAllActivePayGrades - Test.");
            IEnumerable<PayGrade> payGrades = _context.PayGrades.Where(x => x.IsActive).ToList();
            try
            {
                Assert.That(payGrades.Count(), Is.EqualTo(4));
                _logger.LogInformation("GetAllActivePayGrades - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "GetAllActivePayGrades - Test finished with error. \r\n");
            }
        }
    }
}