using LayerTestApp.Payroll.BAL.Mapper;
using LayerTestApp.Payroll.BAL.ServiceContracts;
using LayerTestApp.Payroll.BAL.Services;
using LayerTestApp.Payroll.DAL.Repositories;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
