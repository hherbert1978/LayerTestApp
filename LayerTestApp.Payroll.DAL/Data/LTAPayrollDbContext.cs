using LayerTestApp.Payroll.DAL.Configuration;
using LayerTestApp.Payroll.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LayerTestApp.Payroll.DAL.Data
{
    public class LTAPayrollDbContext : DbContext
    {
        public LTAPayrollDbContext(DbContextOptions<LTAPayrollDbContext> options) : base(options) { }

        public DbSet<PayGradeDAL> PayGrades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var schema = configuration.GetValue<string>("Database:DefaultSchemas:LTAPayrollSchema");

            modelBuilder.HasDefaultSchema(schema);

            _ = new PayGradeConfiguration(modelBuilder.Entity<PayGradeDAL>());

        }
    }
}