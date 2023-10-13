using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using OnlineStore.Data;
using OnlineStore.IRepository;

namespace OnlineStore.Repository
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        //implementation of the methods in ICartRepository. This Class handles logic directed at the database. 
        public OnlineStoreDbContext _context;
        public CartRepository(OnlineStoreDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Cart> GetDetailsById(int id)
        {
            //finds data of carts by cartId, and include information about products
            return await _context.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }  
        public async Task<List<Cart>> GetDetails()
        {
            //finds data of all carts, include information about products and then save to list 

            return await _context.Carts.Include(c => c.Products).ToListAsync();
        }

        public async Task<Cart> GetFirstCartAsync()
        {
            //finds the first Cart and include details about products
            return await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync();
        }

        public async Task<Cart> GetAsync()
        {
            //finds the first Cart and include details about products
            return await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync();
        }

    }
}
