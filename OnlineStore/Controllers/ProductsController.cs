using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Dto.Product;
using OnlineStore.IRepository;
using OnlineStore.Repository;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productsRepository;

        //I inject the IMapper interface, so that i can use the AutoMapperConfig file and Dto-Classes
        public ProductsController(IMapper mapper, IProductsRepository productsRepository)
        {
            _mapper = mapper;
            _productsRepository = productsRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetProducts()
        {
            //henter data fra databasen og lagrer produktene i listen products.
            var products = await _productsRepository.GetAllAsync();
        
          //Tar informasjonen jeg fikk fra databasen og mapper den om til Dto. Dette blir lagret i en liste av dto-objekter som jeg har kalt records.
          var productDtos = _mapper.Map<List<GetProductDto>>(products);
            
          //returnere listen av produkter via dto-objekter: 
          return Ok(productDtos);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProduct(int id)
        {
            //henter data fra databasen, lagrer det i variabelen product
            var product = await _productsRepository.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            //mapper product-objektet om til et dto-objekt
            var productDto = _mapper.Map<GetProductDto>(product);

            return Ok(productDto);
        }

        ////////////////////TRENGER IKKE DISSE FOR OPPGAVEN://////////////////////////////////

        //// PUT: api/Products/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProduct(int id, Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Products
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Product>> PostProduct(Product product)
        //{
        //  if (_context.Products == null)
        //  {
        //      return Problem("Entity set 'OnlineStoreDbContext.Products'  is null.");
        //  }
        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        //}

        //// DELETE: api/Products/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    if (_context.Products == null)
        //    {
        //        return NotFound();
        //    }
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ProductExists(int id)
        //{
        //    return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
