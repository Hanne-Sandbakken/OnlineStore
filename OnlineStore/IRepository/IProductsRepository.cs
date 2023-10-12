using OnlineStore.Data;

namespace OnlineStore.IRepository
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        //kontrakt for Products
        //Task<Cart> GetProductByIdAsync();


    }
}
