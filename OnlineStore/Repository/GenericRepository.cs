using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.IRepository;
using System.Threading.Channels;

namespace OnlineStore.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //implementation of the methods in IGenericRepository. This Class handles logic directed at the database. 

        private readonly OnlineStoreDbContext _context; 
        private readonly IMapper _mapper;

        public GenericRepository(OnlineStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
        {
            //Maps source to T
            var entity = _mapper.Map<T>(source);

            //Adds entity to database
            await _context.AddAsync(entity);
            // saves changes in database:
            await _context.SaveChangesAsync();

            //map the entity to a dto, and returns dto. 
            return _mapper.Map<TResult>(entity);
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

        public async Task<List<TResult>> GetAllAsync<TResult>()
        {
            //Returns a list of TResult: 
            return await _context.Set<T>()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider) //maps T to type TResult.
                .ToListAsync();
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
        {
            //find the total size:
            var totalSize = await _context.Set<T>().CountAsync();

            //get the items, skippin to the startindex:
            var items = await _context.Set<T>() //which table
                .Skip(queryParameters.StartIndex) //where it should start
                .Take(queryParameters.PageSize) //take records from
                .ProjectTo<TResult>(_mapper.ConfigurationProvider) //The exact column it should query//whenever you do this projection, just look to the configurationProvider. Differece between TResult and T: T represent the model, TResult represent the dto.  
                .ToListAsync(); //executes query
            return new PagedResult<TResult>
            {
                Items = items,
                PageNumber = queryParameters.PageNumber,
                RecordNumber = queryParameters.PageSize,
                TotalCount = totalSize,
            };

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

        public async Task<TResult> GetAsync<TResult>(int? id)
        {
            //finds the object in database with id:
            var result = await _context.Set<T>().FindAsync(id);

            if (result == null)
            {
                //throw new NotFoundException();
                return default;
            }

            //maps the object to a TResult and returns it.
            return _mapper.Map<TResult>(result);
        }

        public async Task UpdateAsync(T entity)
        {
            //used in PostProductToCart in CartsController. Updates an entity and saves changes. 
            _context.Update(entity);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync<TSource>(int id, TSource source)
        {
            //gets the entity with id:
            var entity = await GetAsync(id);

            if (entity == null)
            {
                throw new DirectoryNotFoundException();
            }

            //maps source to entity
            _mapper.Map(source, entity); 
            
            //updates entity
            _context.Update(entity);

            //saves changes
            await _context.SaveChangesAsync();
        }
    }
}
