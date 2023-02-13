using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Models;

namespace LayerTestApp.Payroll.DAL.Tests
{
    [TestFixture, Order(1)]
    public class ContextTests
    {
        private LTAPayrollDbContext LtaPayrollDbContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Information("------------------------------------------------------------------------------------------");
            Log.Information("Starting ContextTests.");
            Log.Information("------------------------------------------------------------------------------------------\r\n");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Log.Information("------------------------------------------------------------------------------------------");
            Log.Information("Finishing ContextTests.");
            Log.Information("------------------------------------------------------------------------------------------\r\n");
        }

        [SetUp]
        public void Setup()
        {
            string[] args = Array.Empty<string>();
            LTAPayrollDbContextFactory ltaPayrollContextFactory = new();
            LtaPayrollDbContext = ltaPayrollContextFactory.CreateDbContext(args);
        }

        [Test, Order(1)]
        public void GetAllPayGrades()
        {
            Log.Information("Starting GetAllPayGrades - Test.");
            IEnumerable<PayGrade> payGrades = LtaPayrollDbContext.PayGrades.ToList();
            try
            {
                Assert.That(payGrades.Count(), Is.EqualTo(5));
                Log.Information("GetAllPayGrades - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "GetAllPayGrades - Test finished with error. \r\n");
            }
        }

        [Test, Order(2)]
        public void GetAllActivePayGrades()
        {
            Log.Information("Starting GetAllActivePayGrades - Test.");
            IEnumerable<PayGrade> payGrades = LtaPayrollDbContext.PayGrades.Where(x => x.IsActive).ToList();
            try
            {
                Assert.That(payGrades.Count(), Is.EqualTo(4));
                Log.Information("GetAllActivePayGrades - Test finished successfully. \r\n");
            }
            catch (Exception ex)
            {
                Log.Information(ex, "GetAllActivePayGrades - Test finished with error. \r\n");
            }
        }
    }
}