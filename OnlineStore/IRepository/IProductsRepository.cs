using OnlineStore.Data;

namespace OnlineStore.IRepository
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        //Interface for Products. Classes that uses this interface have to implement thise methods that are spesific for Products.

    }
}
