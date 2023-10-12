using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using OnlineStore.Data;
using OnlineStore.IRepository;

namespace OnlineStore.Repository
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public OnlineStoreDbContext _context;
        public CartRepository(OnlineStoreDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Cart> GetDetailsById(int id)
        {
            return await _context.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }  
        public async Task<List<Cart>> GetDetails()
        {
           
            return await _context.Carts.Include(c => c.Products).ToListAsync();
        }

        public async Task<Cart> GetFirstCartAsync()
        {
            return await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync();
        }

        public async Task<Cart> GetAsync()
        {
            return await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync();
        }

    }
}
