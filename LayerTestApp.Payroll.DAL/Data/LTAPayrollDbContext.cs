using LayerTestApp.Payroll.DAL.Configuration;
using LayerTestApp.Payroll.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public new int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>().ToList())
            {
                switch (entry.State)
                {

                    case EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        break;
                }
            }

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
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        entry.State = EntityState.Modified;
                        break;
                }

            }

            return base.SaveChanges();
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>().ToList())
            {
                switch (entry.State)
                {

                    case EntityState.Deleted:
                        entry.Entity.IsDeleted = true;
                        break;
                }
            }

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
                        entry.Entity.DeletedAt= DateTime.UtcNow;
                        entry.State = EntityState.Modified;
                        break;
                }

            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}