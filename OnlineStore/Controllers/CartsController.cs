using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Dto.Cart;
using OnlineStore.Dto.Product;
using OnlineStore.IRepository;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;


        public CartsController(IMapper mapper, ICartRepository cartRepository, IProductsRepository productsRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _productsRepository = productsRepository;

        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCartDto>>> GetCarts()
        {
            //Hent data fra databasen:
            var carts = await _cartRepository.GetDetails();

            //Tar informasjon fra databasen og mapper den til dto:
            var cartDtos = _mapper.Map<List<GetCartDto>>(carts);

            //returnerer listern av produkter via dto-objekter:
            return Ok(cartDtos);

            //if (_context.Carts == null)
            //{
            //    return NotFound();
            //}
            //var cart = await _context.Carts.Include(c => c.Products).ToListAsync();
            //return cart;
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCartDto>> GetCart(int id)
        {
            var cart = await _cartRepository.GetDetailsById(id);

            if (cart == null)
            {
                return NotFound();
            }
             var cartDto = _mapper.Map<GetCartDto>(cart);

            return Ok(cartDto);
        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostProductToCart(PostToCartDto postToCartDto)
        {
            //////////////METODEN ER FEIL//////////////////////////////
            var cart = await _cartRepository.GetFirstCartAsync();//.GetAsync();//.GetDetailsById(postToCartDto.Id);

            if (cart == null)
            {
                cart = new Cart();
                await _cartRepository.AddAsync(cart);
            }

            var productToAdd = await _productsRepository.GetAsync(postToCartDto.Id);
            if (cart.Products == null)
            {
                cart.Products = new List<Product>();
            }
            cart.Products.Add(productToAdd);
            cart.TotalPrice = cart.Products.Sum(p => p.Price);

            ///HER ER DET FEIL:
            await _cartRepository.UpdateAsync(cart);

            return Ok(productToAdd.Name + " added to cart");
            //await _cartRepository.AddAsync(productToAdd);
            //var productToAdd = _mapper.Map<Product>(postToCartDto);

            //if (cart == null)
            //{
            //    cart = new Cart();
            //    _context.Carts.Add(cart);
            //}

            //await _cartRepository.AddAsync(productToAdd);
            //return Ok(productToAdd);

            //var cart = await _context.Carts.Include(c => c.Products)
            //                                    .FirstOrDefaultAsync();
            //if (cart == null)
            //{
            //    cart = new Cart();
            //    _context.Carts.Add(cart);
            //}

            //var productToAdd = await _context.Products.FirstOrDefaultAsync(p => p.Id == postToCartDto.Id);

            ////Hvis Productslisten i cart ikke eksisterer, oppretter vi en ny liste. 
            //if (cart.Products == null)
            //{
            //    cart.Products = new List<Product>();
            //}
            ////cart.Products ??= new List<Product>();

            //cart.Products.Add(productToAdd);
            //cart.TotalPrice = cart.Products.Sum(p => p.Price);

            //await _context.SaveChangesAsync();
            //return Ok(productToAdd.Name + " added to cart");
        }

        /////////////TRENGER IKKE DISSE FOR OPPGAVEN//////////////////////////

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

        //    // POST: api/Carts
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<Cart>> PostProductToCart(PostToCartDto postToCartDto)
        //    {

        //        var cart = await _context.Carts.Include(c => c.Products)
        //                                            .FirstOrDefaultAsync();
        //        if (cart == null)
        //        {
        //            cart = new Cart();
        //            _context.Carts.Add(cart);
        //        }

        //        var productToAdd = await _context.Products.FirstOrDefaultAsync(p => p.Id == postToCartDto.Id);

        //        //Hvis Productslisten i cart ikke eksisterer, oppretter vi en ny liste. 
        //        if (cart.Products == null)
        //        {
        //            cart.Products = new List<Product>();
        //        }
        //        //cart.Products ??= new List<Product>();

        //        cart.Products.Add(productToAdd);
        //        cart.TotalPrice = cart.Products.Sum(p => p.Price);



        //        await _context.SaveChangesAsync();
        //        return Ok(productToAdd.Name + " added to cart");
        //    }
        //    //    // POST: api/Carts
        //    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    //    [HttpPost]
        //    //public async Task<ActionResult<PostToCartDto>> PostProductToCart(PostToCartDto productDto)
        //    //{
        //    //    var productsToAdd = _mapper.Map<Product>(productDto);
        //    //    await _context.AddAsync(productsToAdd);

        //    //   // var cart = await _context.Carts.Include(c => c.Products)
        //    //   //                                     .FirstOrDefaultAsync();

        //    //   // if (cart == null)
        //    //   // {
        //    //   //     cart = new Cart();
        //    //   //     _context.Carts.Add(cart);
        //    //   // }

        //    //   // var productToAdd = _mapper.Map<Product>(productDto);  

        //    //   //// var productToAdd = await _context.Products.FirstOrDefaultAsync(p => p.Id ==  productDto.Id);


        //    //   // //Hvis Productslisten i cart ikke eksisterer, oppretter vi en ny liste. 
        //    //   // cart.Products ??= new List<Product>();

        //    //   // cart.Products.Add(productToAdd);
        //    //   // cart.TotalPrice = cart.Products.Sum(p => p.Price);



        //    //   // await _context.SaveChangesAsync();
        //    //   // return Ok(productToAdd.Name + " added to cart");
        //    //}

        //    // DELETE: api/Carts/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteCart(int id)
        //    {
        //        if (_context.Carts == null)
        //        {
        //            return NotFound();
        //        }
        //        var cart = await _context.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        //        if (cart == null)
        //        {
        //            return NotFound();
        //        }

        //        var productsInCart = _context.Products.Where(p => p.Id == cart.Id);

        //        foreach ( var product in productsInCart)
        //        {
        //            product.CartId = null;
        //        }

        //        _context.Carts.Remove(cart);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    //private bool CartExists(int id)
        //    //{
        //    //    return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        //    //}
    }
}
