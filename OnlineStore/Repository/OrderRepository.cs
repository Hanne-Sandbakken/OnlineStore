using OnlineStore.Data;
using OnlineStore.IRepository;

namespace OnlineStore.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OnlineStoreDbContext _context;
        public OrderRepository(OnlineStoreDbContext context) : base(context)
        {
        }

        
    }
}
