namespace RpgApi.Infrastructure.Interfaces
{
    public interface IReadUpdateService<T, in TIn>
    {
        T GetByCharacterId(TIn id);
        void Update(TIn id, T item);
    }
}