using LayerTestApp.Payroll.BAL.Mapper;
using LayerTestApp.Payroll.BAL.ServiceContracts;
using LayerTestApp.Payroll.BAL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LayerTestApp.Payroll.BAL
{
    public static class PayrollBALDependencyInjection
    {
        public static IServiceCollection AddPayrollBALDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(IAutoMapperMarker));
            services.AddServices();

            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPayGradeService, PayGradeService>();
        }
    }
}
