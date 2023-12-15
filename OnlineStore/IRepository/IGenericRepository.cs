using OnlineStore.Data;

namespace OnlineStore.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        //As a contract every class that use this interface must implement. 

        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
    }
}
