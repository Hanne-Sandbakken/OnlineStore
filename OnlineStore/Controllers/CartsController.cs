using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly OnlineStoreDbContext _context;

        public CartsController(OnlineStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            //var cart = await _context.Carts.ToListAsync();
            var cart = await _context.Carts.Include(c => c.Products).ToListAsync();
            return cart;
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        //// PUT: api/Carts/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCart(int id, Cart cart)
        //{
        //    if (id != cart.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(cart).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostProductToCart(int productId)
        {
            var cart = await _context.Carts.Include(c => c.Products) 
                                                .FirstOrDefaultAsync();
            if (cart == null)
            {
                cart = new Cart();
                _context.Carts.Add(cart);
            }

            var productToAdd = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (productToAdd == null)
            {
                return NotFound("Product not found.");
            }
            cart.Products ??= new List<Product>();

            cart.Products.Add(productToAdd);
            cart.TotalPrice = cart.Products.Sum(p => p.Price);



            await _context.SaveChangesAsync();
            return Ok(productToAdd.Name + " added to cart");

            //_context.Carts.Add(product);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }   
        //// POST: api/Carts
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Cart>> PostCart(Cart cart)
        //{
        //  if (_context.Carts == null)
        //  {
        //      return Problem("Entity set 'OnlineStoreDbContext.Carts'  is null.");
        //  }
        //    _context.Carts.Add(cart);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        //}

        //// DELETE: api/Carts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCart(int id)
        //{
        //    if (_context.Carts == null)
        //    {
        //        return NotFound();
        //    }
        //    var cart = await _context.Carts.FindAsync(id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Carts.Remove(cart);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool CartExists(int id)
        {
            return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
