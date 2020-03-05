using System.Collections.Generic;

namespace RpgApi.Infrastructure.Interfaces
{
    public interface ICrudService<T, in TId>
    {
        IEnumerable<T> GetAll();
        T GetById(TId id);
        T Create(T item);
        void Update(TId id, T item);
        void Delete(TId id);
    }
}