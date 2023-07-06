using LayerTestApp.Payroll.DAL.Entities;
using Microsoft.EntityFrameworkCore;
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
            _logger.LogInformation("------------------------------------------------------------------------------------------");
            _logger.LogInformation("Starting TestSetup DAL.");
            _logger.LogInformation("------------------------------------------------------------------------------------------\r\n");

            // Create new Test-Schema
            _context.Database.EnsureCreated();
            CreateTestData();
            _logger.LogInformation("Database schema '{DefaultSchema}' created. \r\n", _defaultSchema);
        }

        [OneTimeTearDown]
        public void OneTimeRootTearDown()
        {
            // Delete Test-Schema
            _context.Database.ExecuteSqlRaw($"DROP SCHEMA IF EXISTS {_defaultSchema} CASCADE");
            _logger.LogInformation("Database schema '{DefaultSchema}' deleted.\r\n", _defaultSchema);

            _logger.LogInformation("------------------------------------------------------------------------------------------");
            _logger.LogInformation("Finishing TestSetup DAL.");
            _logger.LogInformation("------------------------------------------------------------------------------------------\r\n");
        }

        private void CreateTestData()
        {
            _context.PayGrades.Add(new PayGrade { PayGradeName = "Meister" });
            _context.PayGrades.Add(new PayGrade { PayGradeName = "Geselle" });
            _context.PayGrades.Add(new PayGrade { PayGradeName = "Lehrling" });
            _context.PayGrades.Add(new PayGrade { PayGradeName = "Feldarbeiter" });
            _context.PayGrades.Add(new PayGrade { PayGradeName = "Hilfsarbeiter", IsActive = false });
            _context.PayGrades.Add(new PayGrade { PayGradeName = "Aushilfe", IsActive = false, IsDeleted = true, DeletedAt = DateAndTime.DateAdd(DateInterval.Second, 30, DateTime.UtcNow) });
            _context.SaveChanges();
        }
    }
}
