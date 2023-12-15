using OnlineStore.IRepository;
using OnlineStore.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;



namespace OnlineStore.Repository
{
    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {

        //a constructor that take a copy of the dbContext, and pass it on to the base:
        public OnlineStoreDbContext _context;
        public ProductsRepository(OnlineStoreDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

    }
}
