using LayerTestApp.Payroll.DAL.Contracts;
using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Entities;
using LayerTestApp.Payroll.DAL.Mapper;
using LayerTestApp.Payroll.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LayerTestApp.Payroll.DAL.Repositories
{
    public class PayGradeRepository : IRepository<PayGrade>
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

        public async Task<List<PayGrade>> GetAllAsync(bool filterForActive = true, CancellationToken ct = default)
        {
            var data = await _context.PayGrades.ToListAsync(ct);
            var filteredData = filterForActive ? data.Where(x => x.IsActive).ToList() : data;

            return Mapping.Mapper.Map<List<PayGrade>>(filteredData);
        }

        public async Task<PayGrade> GetByIdAsync(int id, bool filterForActive = true, CancellationToken ct = default)
        {
            var data = await _context.PayGrades.ToListAsync(ct);
            var payGradeDal = data.Where(x => x.PayGradeId == id);
            var filteredData = filterForActive ? (payGradeDal ?? payGradeDal.Where(x => x.IsActive)) : payGradeDal;

            return Mapping.Mapper.Map<PayGrade>(filteredData.FirstOrDefault());
        }

        public async Task<PayGrade> AddAsync(PayGrade payGrade, CancellationToken ct = default)
        {
            _logger.Log(LogLevel.Information, "Creating new PayGrade with name: \"{PayGradeName}\".", payGrade.PayGradeName);

            var payGradeDAL = Mapping.Mapper.Map<PayGradeDAL>(payGrade);
            var newPayGradeDAL = await _context.PayGrades.AddAsync(payGradeDAL, ct);
            _context.SaveChanges();

            _logger.Log(LogLevel.Information, "A new PayGrade with name: \"{PayGradeName}\" was created.", newPayGradeDAL.Entity.PayGradeName);

            return Mapping.Mapper.Map<PayGrade>(newPayGradeDAL.Entity);
        }

        public async Task<PayGrade> UpdateAsync(PayGrade _object, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(PayGrade _object, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose() => GC.SuppressFinalize(this);

    }
}
