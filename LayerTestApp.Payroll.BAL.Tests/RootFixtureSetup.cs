﻿using LayerTestApp.Payroll.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace LayerTestApp.Payroll.BAL.Tests
{
    [SetUpFixture]
    public class RootFixtureSetup : BaseTestClass
    {
        [OneTimeSetUp]
        public void OneTimeRootSetup()
        {
            Logger.LogInformation("------------------------------------------------------------------------------------------");
            Logger.LogInformation("Starting TestSetup BAL.");
            Logger.LogInformation("------------------------------------------------------------------------------------------\r\n");

            // Create new Test-Schema
            LtaPayrollDbContext.Database.EnsureCreated();
            CreateTestData();
            Logger.LogInformation("Database schema \"{DefaultSchema}\" created. \r\n", DefaultSchema);
        }

        [OneTimeTearDown]
        public void OneTimeRootTearDown()
        {
            // Delete Test-Schema
            LtaPayrollDbContext.Database.ExecuteSqlRaw($"DROP SCHEMA IF EXISTS {DefaultSchema} CASCADE");
            Logger.LogInformation("Database schema \"{DefaultSchema}\" deleted.", DefaultSchema);

            Logger.LogInformation("------------------------------------------------------------------------------------------");
            Logger.LogInformation("Finishing TestSetup BAL.");
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
