using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.IRepository;

namespace OnlineStore.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //implementation of the methods in IGenericRepository. This Class handles logic directed at the database. 

        private readonly OnlineStoreDbContext _context; 

        public GenericRepository(OnlineStoreDbContext context)
        {
            _context = context;
        }

        //
        public async Task<T> AddAsync(T entity)
        {
            //add entity to the database: 
            await _context.AddAsync(entity);

            //saves changes in database
            await _context.SaveChangesAsync();

            //returns the entity
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            //finds the entity in database with id
            var entity = await GetAsync(id);

            //remove entity from database
            _context.Set<T>().Remove(entity);

            //save changes:
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            //gets data of entity with id, returns true if not null, false if null. 
            //this method is not used in the application, but it can be useful in the future. 
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            //used in GetProducts in ProductsController. Returns a list of objects. 
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            //used in PostProductToCart() in CartsController and GetProduct() in ProductsController
            //finds and returns object with id. 
            if (id == null)
            {
                return null;
            }
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            //used in PostProductToCart in CartsController. Updates an entity and saves changes. 
            _context.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
