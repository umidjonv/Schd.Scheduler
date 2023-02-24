namespace Schd.API.Data.Interfaces
{
    public interface ICrudeRepository<T> : IDisposable where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        void DeleteById(long id);
        void Save();
        Task SaveAsync();
    }
}
