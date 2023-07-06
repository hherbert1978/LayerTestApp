using LayerTestApp.Payroll.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace LayerTestApp.Payroll.DAL.Data
{
    public class LTAPayrollDbContext : DbContext
    {
        public LTAPayrollDbContext(DbContextOptions<LTAPayrollDbContext> options) : base(options) { }

        public DbSet<PayGrade> PayGrades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var appFile = "appsettings";
            switch (environmentName)
            {
                case "Test":
                    appFile += ".test.json";
                    break;
                case "Development":
                    appFile += ".dev.json";
                    break;
                case "Production":
                    appFile += ".prod.json";
                    break;
            }

            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(appFile)
               .Build();

            var schema = configuration["Database:DefaultSchemas:LTAPayrollSchema"];

            modelBuilder.HasDefaultSchema(schema);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public new int SaveChanges()
        {
            //foreach (var entry in ChangeTracker.Entries<BaseModel>().ToList())
            //{
            //    switch (entry.State)
            //    {
            //        case EntityState.Deleted:
            //            entry.Entity.IsDeleted = true;
            //            break;
            //    }
            //}

            foreach (var entry in ChangeTracker.Entries<BaseModel>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
            return base.SaveChanges();
        }

        public new async Task<int> SaveChangesAsync(CancellationToken ct = new())
        {
            //foreach (var entry in ChangeTracker.Entries<BaseModel>().ToList())
            //{
            //    switch (entry.State)
            //    {
            //        case EntityState.Deleted:
            //            entry.Entity.IsDeleted = true;
            //            break;
            //    }
            //}

            foreach (var entry in ChangeTracker.Entries<BaseModel>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        entry.State = EntityState.Modified;
                        break;
                }

            }
            return await base.SaveChangesAsync(ct);
        }

    }
}