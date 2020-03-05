using System.Threading.Tasks;

namespace RpgApi.Infrastructure.Interfaces
{
    public interface IReadUpdateServiceAsync<T, in TIn>
    {
        Task<T> GetByCharacterIdAsync(TIn id);
        Task UpdateAsync(TIn id, T item);
    }
}