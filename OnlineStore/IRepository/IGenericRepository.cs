using Microsoft.Identity.Client;
using OnlineStore.Data;

namespace OnlineStore.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        //As a contract every class that use this interface must implement. 

        Task<T> GetAsync(int? id);
        Task<TResult?> GetAsync<TResult>(int? id); 

        Task<List<T>> GetAllAsync();
        Task<List<TResult>> GetAllAsync<TResult>(); //use this to be able to map dto in repository
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters); 

        Task<T> AddAsync(T entity);
        Task<TResult> AddAsync<TSource, TResult>(TSource source); //use this to be able to map dto in repository

        Task DeleteAsync(int id);

        Task UpdateAsync(T entity);
        Task UpdateAsync<TSource>(int id, TSource source); //use this to be able to map dto in repository
    }
}
