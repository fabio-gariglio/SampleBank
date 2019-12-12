using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleBank.Infrastructure
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(string id);

        Task SaveAsync(T entity, string id);
    }
}