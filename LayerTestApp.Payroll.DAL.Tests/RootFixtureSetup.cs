using LayerTestApp.Common.Logging;
using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace LayerTestApp.Payroll.DAL.Tests
{

    [SetUpFixture]
    public class RootFixtureSetup : BaseTestClass
    {
        [OneTimeSetUp]
        public void OneTimeRootSetup()
        {
            Logger.LogInformation("------------------------------------------------------------------------------------------");
            Logger.LogInformation("Starting TestSetup DAL.");
            Logger.LogInformation("------------------------------------------------------------------------------------------\r\n");

            // Create new Test-Schema
            LtaPayrollDbContext.Database.EnsureCreated();
            CreateTestData();
            Logger.LogInformation($"Database schema \"{DefaultSchema}\" created. \r\n");
        }

        [OneTimeTearDown]
        public void OneTimeRootTearDown()
        {
            // Delete Test-Schema
            LtaPayrollDbContext.Database.ExecuteSqlRaw($"DROP SCHEMA IF EXISTS {DefaultSchema} CASCADE");
            Logger.LogInformation($"Database schema \"{DefaultSchema}\" deleted.");

            Logger.LogInformation("------------------------------------------------------------------------------------------");
            Logger.LogInformation("Finishing TestSetup DAL.");
            Logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        private void CreateTestData()
        {
            LtaPayrollDbContext.PayGrades.Add(new PayGrade { PayGradeName = "Meister" });
            LtaPayrollDbContext.PayGrades.Add(new PayGrade { PayGradeName = "Geselle" });
            LtaPayrollDbContext.PayGrades.Add(new PayGrade { PayGradeName = "Lehrling" });
            LtaPayrollDbContext.PayGrades.Add(new PayGrade { PayGradeName = "Feldarbeiter" });
            LtaPayrollDbContext.PayGrades.Add(new PayGrade { PayGradeName = "Hilfsarbeiter", IsActive = false });
            LtaPayrollDbContext.PayGrades.Add(new PayGrade { PayGradeName = "Aushilfe", IsActive = false, IsDeleted = true, DeletedAt = DateAndTime.DateAdd(DateInterval.Second, 30, DateTime.UtcNow) });
            LtaPayrollDbContext.SaveChanges();
        }
    }
}
