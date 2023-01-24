using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Payroll.DAL.Data
{
    public class LTAPayrollDbContextFactory : IDesignTimeDbContextFactory<LTAPayrollDbContext>
    {
        public LTAPayrollDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string schema = configuration.GetValue<string>("Database:DefaultSchemas:LTAPayrollSchema");

            var builder = new DbContextOptionsBuilder<LTAPayrollDbContext>()
                .UseNpgsql(
                    configuration.GetValue<string>("Database:ConnectionStrings:LTAPayrollDBConnection"),
                    b =>
                    {
                        
                        b.MigrationsHistoryTable("__Payroll_Migrations", schema: schema);
                    }
                );

            return new LTAPayrollDbContext(builder.Options);
        }
    }
}
