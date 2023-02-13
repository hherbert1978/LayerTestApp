using System.Linq.Expressions;

namespace LayerTestApp.Payroll.DAL.RepositoryContracts
{
    public interface IBaseRepository<T> : IDisposable
    {
        Task<List<T>> GetAllAsync(CancellationToken ct = default);

        Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate,
                                       CancellationToken ct = default);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
                                       CancellationToken ct = default);

        Task<T> AddAsync(T entity,
                         CancellationToken ct = default);

        Task<T> UpdateAsync(T entity,
                            CancellationToken ct = default);

        Task<bool> DeleteAsync(T entity,
                               CancellationToken ct = default);

    }
}
