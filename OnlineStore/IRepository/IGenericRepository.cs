namespace OnlineStore.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        //kommuniserer med databasen på våres vegne. Det som blir definert her, må alle arvende klasser følge

        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task <bool> Exists(int id);
        //sk<T> UpdateAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
