using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.IRepository;

namespace OnlineStore.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //faktisk implementasjon av repository, arver fra kontrakten vi har i IGenereicRepositroy

        private readonly OnlineStoreDbContext _context; 

        public GenericRepository(OnlineStoreDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            //hent informasjon, hvis den ikke er null, hvis den ikke er null, returnerer den true. Er den null, returnerer den false 
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
           if (id == null)
            {
                return null;
            }
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}
