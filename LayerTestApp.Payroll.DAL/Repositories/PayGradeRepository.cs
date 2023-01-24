using LayerTestApp.Payroll.DAL.Contracts;
using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Repositories
{
    public class PayGradeRepository : IRepository<PayGradeDAL>
    {
        private readonly ILogger _logger;
        private readonly LTAPayrollDbContext _context;

        public PayGradeRepository(ILogger<PayGradeRepository> logger,
                                  LTAPayrollDbContext context)
        {
            _logger = logger;
            _context = context;
            //SerilogConfigurationHelper.ConfigureForFile("LayerTestApp.DAL.Payroll.Repositories.PayGradeRepository", "RepositoryLog.txt");
        }

        public async Task<List<PayGradeDAL>> GetAllAsync(bool filterForActive = true, CancellationToken ct = default)
        {
            var data = await _context.PayGrades.ToListAsync(ct);
            var filteredData = filterForActive ? data.Where(x => x.IsActive).ToList() : data;
            return filteredData;
        }

        public async Task<PayGradeDAL> GetByIdAsync(int id, bool filterForActive = true, CancellationToken ct = default)
        {
            var data = await _context.PayGrades.ToListAsync(ct);
            var payGrade = data.Where(x => x.PayGradeId == id);
            var filteredData = filterForActive ? (payGrade ?? payGrade.Where(x => x.IsActive)) : payGrade;

            return filteredData.FirstOrDefault();
        }

        public async Task<PayGradeDAL> AddAsync(PayGradeDAL _object, CancellationToken ct = default)
        {


            _logger.Log(LogLevel.Information, "A new new PayGrade was created");
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(PayGradeDAL _object, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(PayGradeDAL _object, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose() => GC.SuppressFinalize(this);

    }
}
