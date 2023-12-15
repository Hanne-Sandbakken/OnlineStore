using AutoMapper;
using OnlineStore.Data;
using OnlineStore.IRepository;

namespace OnlineStore.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OnlineStoreDbContext _context;
        //a constructor that take a copy of the dbContext, and pass it on to the base:
        public OrderRepository(OnlineStoreDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        
    }
}
