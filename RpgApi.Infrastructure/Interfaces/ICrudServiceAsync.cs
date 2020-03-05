using System.Collections.Generic;
using System.Threading.Tasks;

namespace RpgApi.Infrastructure.Interfaces
{
    public interface ICrudServiceAsync<T, in TId>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(TId id);
        Task<T> Create(T item);
        Task DeleteAsync(TId id);
    }
}