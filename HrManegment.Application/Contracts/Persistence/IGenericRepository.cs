using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Contracts.Persistence
{
    public interface IGenericRepository<T>where T : class
    {
        Task<IReadOnlyList<T>> GetAsync(int PageNumber = 1, int Count = 10);
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
