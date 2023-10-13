using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Dto.Cart;
using OnlineStore.Dto.Product;
using OnlineStore.IRepository;

namespace OnlineStore.Controllers
{
    [ApiController]
    public class CartsController : ControllerBase
    {
        //Controller accepts and processes HTTP requests, orchestrating the application's response. 
        //The database should be distracted from the controller, and the endpoints should not contain much logic.
        //I have therefor used Repository that handle the logic, and the contoller should redirect to find the logic in IRepository and Repository.
        //IMapper is used to map dto to models. It translate the dto to Product-object. I use different Dto-klasses for different endpoints, depending on what information of the object it need to do the job, or what information i want to give the user. 
        
        private readonly ICartRepository _cartRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;


        //I inject the IMapper interface, so that i can use the AutoMapperConfig file and Dto-Classes
        //IProductRepository handle the logic
        public CartsController(IMapper mapper, ICartRepository cartRepository, IProductsRepository productsRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _productsRepository = productsRepository;

        }

        // GET: api/Carts
        [HttpGet]
        [Route("api/cart")] 
        //Gets the Carts in the database and show a list of products. 
        public async Task<ActionResult<IEnumerable<GetCartDto>>> GetCarts()
        {
            //Gets cart-objects  with information about products from database, save it in the list carts:
            var carts = await _cartRepository.GetDetails();

            //Maps the information from database to dtos:
            var cartDtos = _mapper.Map<List<GetCartDto>>(carts);

            //returns the list of cartDtos:
            return Ok(cartDtos);
        }

        // GET: api/Carts/5
        [HttpGet]
        [Route("api/cart/{id}")]
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
        [Route("api/cart")]
        public async Task<ActionResult<Cart>> PostProductToCart(PostToCartDto postToCartDto)
        {
            //finds the cart from database. If the database, make a new Cart and add it to database. 
            var cart = await _cartRepository.GetAsync();
            if (cart == null)
            {
                cart = new Cart();
                await _cartRepository.AddAsync(cart);
            }

            //finds the product we want to add to list of products in cart by id. If the list doesn't exist, make a new one. 
            var productToAdd = await _productsRepository.GetAsync(postToCartDto.Id);
            if (cart.Products == null)
            {
                cart.Products = new List<Product>();
            }

            // add the product to list of products in cart.
            cart.Products.Add(productToAdd);
            cart.TotalPrice = cart.Products.Sum(p => p.Price);

           //Update cart
            await _cartRepository.UpdateAsync(cart);

            // return the message "name of product" + "added to cart".
            return Ok(productToAdd.Name + " added to cart");

        }
    }
}
