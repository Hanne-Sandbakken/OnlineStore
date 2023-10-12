using OnlineStore.Data;

namespace OnlineStore.IRepository
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        //Denne metoden er ikke med i IGenericRepository fordi den ikke trengs i alle klassene og metodene. 
        Task<Cart> GetDetailsById(int id);
        Task<List<Cart>> GetDetails();
        Task<Cart> GetFirstCartAsync();
        Task<Cart> GetAsync();
    }
}
