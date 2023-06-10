using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Entities;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace LayerTestApp.Payroll.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly ILogger _logger;
        protected readonly LTAPayrollDbContext _context;
        protected readonly DbSet<T> _dbSet;

        private readonly string _tType;
        private readonly string _nameProperty;
        private readonly string _idProperty;

        protected BaseRepository(ILogger logger,
                                 LTAPayrollDbContext context)
        {
            _logger = logger;
            _context = context;
            _dbSet = context.Set<T>();

            _tType = typeof(T).Name;
            _nameProperty = typeof(T).GetProperties().Where(x => x.Name == (_tType + "Name")).Select(x => x.Name).FirstOrDefault();
            _idProperty = typeof(T).GetProperties().Where(x => x.Name == (_tType + "Id")).Select(x => x.Name).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        {
            var data = await _dbSet.ToListAsync(ct);
            return data;
        }

        public async Task<IEnumerable<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            var data = await _dbSet.Where(predicate).ToListAsync(ct);
            return data;
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            var data = await _dbSet.Where(predicate).ToListAsync(ct);
            return data.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAllDeletedAsync(CancellationToken ct = default)
        {
            var data = await _dbSet.IgnoreQueryFilters().Where(d => d.IsDeleted).ToListAsync(ct);
            return data;
        }

        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            if (_nameProperty != null)
            {
                string entityValue = typeof(T).GetProperty(_nameProperty).GetValue(entity).ToString();
                _logger.Log(LogLevel.Information, "Creating new {entity} with name: {entityValue}.", _tType, entityValue);
            }
            else
            {
                _logger.Log(LogLevel.Information, "Creating new {entity}.", _tType);
            }

            var newEntity = (await _dbSet.AddAsync(entity, ct)).Entity;
            await _context.SaveChangesAsync(ct);

            if (_nameProperty != null)
            {
                string entityValue = typeof(T).GetProperty(_nameProperty).GetValue(newEntity).ToString();
                _logger.Log(LogLevel.Information, "A new {entity} with name: {entityValue} was created.", _tType, entityValue);
            }
            else
            {
                _logger.Log(LogLevel.Information, "A new {entity} was created.", _tType);
            }

            return newEntity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken ct = default)
        {
            string entityIdValue = typeof(T).GetProperty(_idProperty).GetValue(entity).ToString();
            if (_nameProperty != null)
            {
                string entityNameValue = typeof(T).GetProperty(_nameProperty).GetValue(entity).ToString();
                _logger.Log(LogLevel.Information, "Updating {entity} with id: \"{entityIdValue}\" & name: \"{entityNameValue}\".", _tType, entityIdValue, entityNameValue);
            }
            else
            {
                _logger.Log(LogLevel.Information, "Updating {entity} with id: \"{entityIdValue}\".", _tType, entityIdValue);
            }

            _context.Update(entity);
            await _context.SaveChangesAsync(ct);

            if (_nameProperty != null)
            {
                string entityNameValue = typeof(T).GetProperty(_nameProperty).GetValue(entity).ToString();
                _logger.Log(LogLevel.Information, "{entity} with id: \"{entityIdValue}\" & name: \"{entityNameValue}\" was updated.", _tType, entityIdValue, entityNameValue);
            }
            else
            {
                _logger.Log(LogLevel.Information, "{entity} with id: \"{entityIdValue}\" was updated.", _tType, entityIdValue);
            }

            return entity;
        }

        public async Task<bool> DeleteAsync(T entity, CancellationToken ct = default)
        {
            string entityIdValue = typeof(T).GetProperty(_idProperty).GetValue(entity).ToString();
            if (_nameProperty != null)
            {
                string entityNameValue = typeof(T).GetProperty(_nameProperty).GetValue(entity).ToString();
                _logger.Log(LogLevel.Information, "Deleting {entity} with id: \"{entityIdValue}\" & name: \"{entityNameValue}\".", _tType, entityIdValue, entityNameValue);
            }
            else
            {
                _logger.Log(LogLevel.Information, "Deleting {entity} with id: \"{entityIdValue}\".", _tType, entityIdValue);
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync(ct);

            if (_nameProperty != null)
            {
                string entityNameValue = typeof(T).GetProperty(_nameProperty).GetValue(entity).ToString();
                _logger.Log(LogLevel.Information, "{entity} with id: \"{entityIdValue}\" & name: \"{entityNameValue}\" was deleted.", _tType, entityIdValue, entityNameValue);
            }
            else
            {
                _logger.Log(LogLevel.Information, "{entity} with id: \"{entityIdValue}\" was deleted.", _tType, entityIdValue);
            }

            return true;
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
