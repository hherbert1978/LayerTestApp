using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerTestApp.Payroll.DAL.Contracts
{
    public interface IRepository<T> : IDisposable
    {
        public Task<List<T>> GetAllAsync(bool filterForActive = true,
                                         CancellationToken ct = default);

        public Task<T> GetByIdAsync(int id,
                                    bool filterForActive = true,
                                    CancellationToken ct = default);

        public Task<T> AddAsync(T _object,
                                CancellationToken ct = default);

        public Task<bool> UpdateAsync(T _object,
                                      CancellationToken ct = default);

        public Task<bool> DeleteAsync(T _object,
                                      CancellationToken ct = default);
    
    }
}
