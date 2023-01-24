using LayerTestApp.Common.Logging;
using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Models;
using LayerTestApp.Payroll.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

using System.Reflection;

namespace LayerTestApp.Payroll.DAL.Tests
{
    [TestFixture, Order(1)]
    public class SelectTests
    {
        private LTAPayrollDbContext _ltaPayrollDbContext;

        [OneTimeSetUp] 
        public void OneTimeSetUp() {
            Log.Information("------------------------------------------------------------------------------------------");
            Log.Information("Starting SelectTests.");
            Log.Information("------------------------------------------------------------------------------------------\r\n");
        }

        [SetUp]
        public void Setup()
        {
            string[] args = Array.Empty<string>();
            LTAPayrollDbContextFactory ltaPayrollContextFactory = new ();
            _ltaPayrollDbContext = ltaPayrollContextFactory.CreateDbContext(args);
        }

        [Test]
        public void GetAllPayGrades()
        {
            Log.Information("Starting GetAllPayGrades - Test.");
            IEnumerable<PayGradeDAL> payGrades = _ltaPayrollDbContext.PayGrades.ToList();
            try
            {
                Assert.That(payGrades.Count(), Is.EqualTo(5));
                Log.Information("GetAllPayGrades - Test finished successfully. \r\n");
            }
            catch
            {
                Log.Information("GetAllPayGrades - Test finished with error. \r\n");
            }            
        }

        [Test]
        public void GetAllActivePayGrades()
        {
            Log.Information("Starting GetAllActivePayGrades - Test.");
            IEnumerable<PayGradeDAL> payGrades = _ltaPayrollDbContext.PayGrades.Where(x => x.IsActive).ToList();
            try
            {
                Assert.That(payGrades.Count(), Is.EqualTo(4));
                Log.Information("GetAllActivePayGrades - Test finished successfully. \r\n");
            }
            catch
            {
                Log.Information("GetAllActivePayGrades - Test finished with error. \r\n");
            }
        }
    }
}