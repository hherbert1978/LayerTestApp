using LayerTestApp.Common.Logging;
using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Tests
{

    [SetUpFixture]
    public class RootFixtureSetup
    {
        private readonly IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public LTAPayrollDbContext LtaPayrollDbContext;
        public Microsoft.Extensions.Logging.ILogger Logger;

        [OneTimeSetUp] 
        public void OneTimeRootSetup() {

            var serilog = SerilogConfigurationHelper.ConfigureForFile("DALTests", "Payroll.DAL.TestLog.txt");

            var loggerFactory = new LoggerFactory()
                .AddSerilog(serilog);

            Logger = loggerFactory.CreateLogger("TestsLogger");

            LTAPayrollDbContextFactory contextFactory = new();
            LtaPayrollDbContext = contextFactory.CreateDbContext(null);

            Log.Information("------------------------------------------------------------------------------------------");
            Log.Information("Starting TestSetup.");
            Log.Information("------------------------------------------------------------------------------------------\r\n");

            // First delete Test-Schema
            string dbSchema = Configuration.GetValue<string>("Database:DefaultSchemas:LTAPayrollSchema");
            LtaPayrollDbContext.Database.ExecuteSqlRaw($"DROP SCHEMA IF EXISTS {dbSchema} CASCADE");
            Log.Information($"Database schema \"{dbSchema}\" deleted.");

            // Create new Test-Schema
            LtaPayrollDbContext.Database.EnsureCreated();
            CreateTestData();
            Log.Information("Database schema \"{dbSchema}\" created. \r\n");
        }

        [OneTimeTearDown] 
        public void OneTimeRootTearDown() {
        
        }

        private void CreateTestData()
        {
            LtaPayrollDbContext.PayGrades.Add(new PayGradeDAL { PayGradeName = "Meister" });
            LtaPayrollDbContext.PayGrades.Add(new PayGradeDAL { PayGradeName = "Geselle" });
            LtaPayrollDbContext.PayGrades.Add(new PayGradeDAL { PayGradeName = "Lehrling" });
            LtaPayrollDbContext.PayGrades.Add(new PayGradeDAL { PayGradeName = "Feldarbeiter" });
            LtaPayrollDbContext.PayGrades.Add(new PayGradeDAL { PayGradeName = "Hilfsarbeiter", IsActive = false });
            LtaPayrollDbContext.PayGrades.Add(new PayGradeDAL { PayGradeName = "Aushilfe", IsActive = false, IsDeleted = true });
            LtaPayrollDbContext.SaveChanges();
        }
    }
}
