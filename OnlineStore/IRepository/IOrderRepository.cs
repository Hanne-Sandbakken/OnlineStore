using OnlineStore.Data;

namespace OnlineStore.IRepository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        //Interface for Order. Classes that uses this interface have to implement thise methods that are spesific for Order.

    }
}
