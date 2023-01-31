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
        }

        public async Task<List<PayGrade>> GetAllAsync(bool filterForActive = true, CancellationToken ct = default)
        {
            var data = await _context.PayGrades.ToListAsync(ct);
            var filteredData = filterForActive ? data.Where(x => x.IsActive).ToList()
                                               : data;

            return Mapping.Mapper.Map<List<PayGrade>>(filteredData);
        }

        public async Task<PayGrade> GetByIdAsync(int id, bool filterForActive = true, CancellationToken ct = default)
        {
            var data = await _context.PayGrades.ToListAsync(ct);

            var filteredData = filterForActive ? data.Where(x => x.PayGradeId == id && x.IsActive).FirstOrDefault()
                                               : data.Where(x => x.PayGradeId == id).FirstOrDefault();

            return Mapping.Mapper.Map<PayGrade>(filteredData);
        }

        public async Task<PayGrade> AddAsync(PayGrade payGrade, CancellationToken ct = default)
        {
            _logger.Log(LogLevel.Information, "Creating new PayGrade with name: \"{PayGradeName}\".", payGrade.PayGradeName);

            var payGradeDAL = Mapping.Mapper.Map<PayGradeDAL>(payGrade);
            var newPayGradeDAL = await _context.PayGrades.AddAsync(payGradeDAL, ct);
            await _context.SaveChangesAsync(ct);

            _logger.Log(LogLevel.Information, "A new PayGrade with name: \"{PayGradeName}\" was created.", newPayGradeDAL.Entity.PayGradeName);

            return Mapping.Mapper.Map<PayGrade>(newPayGradeDAL.Entity);
        }

        public async Task<PayGrade> UpdateAsync(PayGrade payGrade, CancellationToken ct = default)
        {
            _logger.Log(LogLevel.Information, "Updating PayGrade with id: \"{PayGradeId}\" & name: \"{PayGradeName}\".", payGrade.PayGradeId.ToString(), payGrade.PayGradeName);

            var data = await _context.PayGrades.ToListAsync(ct);
            var payGradeDAL = data.Where(x => x.PayGradeId == payGrade.PayGradeId).FirstOrDefault();

            if (payGradeDAL == null)
            {
                _logger.Log(LogLevel.Information, "PayGrade with id: \"{PayGradeId}\" not exists, update failed.", payGrade.PayGradeId.ToString());
                throw new KeyNotFoundException($"No PayGrade with id \"{payGrade.PayGradeId}\" exists.");
            }
            else
            {
                payGradeDAL.PayGradeName = payGrade.PayGradeName ?? payGradeDAL.PayGradeName;
                payGradeDAL.IsActive = payGrade.IsActive;
                //payGradeDAL.LastUpdatedAt = DateTime.UtcNow;

                _context.Update(payGradeDAL);
                await _context.SaveChangesAsync(ct);

                _logger.Log(LogLevel.Information, "PayGrade with id: \"{PayGradeId}\" was updated.", payGradeDAL.PayGradeId.ToString());

                return Mapping.Mapper.Map<PayGrade>(payGradeDAL);
            }
        }

        public async Task<bool> DeleteAsync(PayGrade payGrade, CancellationToken ct = default)
        {
            _logger.Log(LogLevel.Information, "Deleting PayGrade with id: \"{PayGradeId}\".", payGrade.PayGradeId.ToString());
            var data = await _context.PayGrades.ToListAsync(ct);
            var payGradeDAL = data.Where(x => x.PayGradeId == payGrade.PayGradeId).FirstOrDefault();

            if (payGradeDAL == null)
            {
                _logger.Log(LogLevel.Information, "PayGrade with id: \"{PayGradeId}\" not exists, delete failed.", payGrade.PayGradeId.ToString());
                throw new KeyNotFoundException($"No PayGrade with id \"{payGrade.PayGradeId}\" exists.");
            }
            else
            {
                //payGradeDAL.DeletedAt = DateTime.UtcNow;

                _context.Remove(payGradeDAL);
                await _context.SaveChangesAsync(ct);
                              
                _logger.Log(LogLevel.Information, "PayGrade with id: \"{PayGradeId}\" was deleted.", payGradeDAL.PayGradeId.ToString());

                return true;
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}