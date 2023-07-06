using Microsoft.Extensions.DependencyInjection;

namespace LayerTestApp.Payroll.DAL.Data
{
    public static class Migration
    {
        public static bool CheckOrCreateDatabase(IServiceProvider serviceProvider)
        {
            var ltaPayrollDbContext = serviceProvider.GetRequiredService<LTAPayrollDbContext>();
            return ltaPayrollDbContext.Database.EnsureCreated();
        }

    }
}
