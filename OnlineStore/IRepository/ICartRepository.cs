using OnlineStore.Data;

namespace OnlineStore.IRepository
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        //interface for Cart. All classes that use this interface must implement thiese methods in addition to the methods in IGenericRepository 
        Task<Cart> GetDetailsById(int id);
        Task<List<Cart>> GetDetails();
        Task<Cart> GetFirstCartAsync();
        Task<Cart> GetAsync();
    }
}
