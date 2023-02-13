using LayerTestApp.Common.Logging;
using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Repositories;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LayerTestApp.Payroll.DAL
{
    public static class PayrollDALDependencyInjection           
    {
        public static IServiceCollection AddPayrollDALDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLTAPayrollDatabase(configuration);
            services.AddLTAPayrollRepositories();
            
            return services;
        }

        private static void AddLTAPayrollDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Database:ConnectionStrings:LTAPayrollDBConnection"];
            services.AddDbContext<LTAPayrollDbContext>(opt =>
                opt.UseNpgsql(connectionString));

        }

        private static void AddLTAPayrollRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPayGradeRepository, PayGradeRepository>();
        }

    }
}
