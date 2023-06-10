using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Entities;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Repositories
{
    public class PayGradeRepository : BaseRepository<PayGrade>, IPayGradeRepository
    {
        public PayGradeRepository(ILogger logger, LTAPayrollDbContext context) : base(logger, context) { }
    }
}
