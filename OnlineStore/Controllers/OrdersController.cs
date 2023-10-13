using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Dto.Order;
using OnlineStore.IRepository;

namespace OnlineStore.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        //OrdersController handle POST-request to checkout from the cart and add an order. I used Repository to distanc some of the logic from the controller. 
       
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;


        // IOrderRepository handle Order, ICartRepository handle the logic for Cart
        public OrdersController(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("checkout")] //I used the name OrderController, and defined the route here to get the wanted path. 
        
        public async Task<ActionResult<Order>> PostOrder(PostCartToOrderDto postCartToOrderDto)
        {
            //this method is used when the user want to checkout the cart and make an order. 

            // find the cart by cartId. Include details about products. If cart is null, return notFound();
            var cart = await _cartRepository.GetDetailsById(postCartToOrderDto.CartId);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            //Create a new order and a new list of products in the order-object. Add it to database
            var order = new Order { Products = new List<Product>(cart.Products), TotalPrice = cart.TotalPrice, DeliveryAdress = postCartToOrderDto.DeliveryAdress };
            await _orderRepository.AddAsync(order);

            return Ok("successfully checked out");
        }
       
    }
}
