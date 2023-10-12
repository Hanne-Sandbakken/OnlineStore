using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Dto.Order;
using OnlineStore.IRepository;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;


        public OrdersController(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        // GET: api/Orders
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        //{
        //  if (_context.Orders == null)
        //  {
        //      return NotFound();
        //  }
        //  var order = await _context.Orders.Include(o => o.Products).ToListAsync();

        //  return order; 
        //}

        //// GET: api/Orders/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Order>> GetOrder(int id)
        //{
        //  if (_context.Orders == null)
        //  {
        //      return NotFound();
        //  }
        //    var order = await _context.Orders.FindAsync(id);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return order;
        //}

        //// PUT: api/Orders/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrder(int id, Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(order).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(PostCartToOrderDto postCartToOrderDto)
        {
            var cart = await _cartRepository.GetDetailsById(postCartToOrderDto.CartId);
            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var order = new Order { Products = new List<Product>(cart.Products), TotalPrice = cart.TotalPrice, DeliveryAdress = postCartToOrderDto.DeliveryAdress };
            await _orderRepository.AddAsync(order);

            return Ok("successfully checked out");
        }
        //// POST: api/Orders
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<PostOrderDto>> PostOrder(PostOrderDto postCartToOrderDto)
        //{
        //    var order = new Order
        //    {
        //        Id = postCartToOrderDto.Id
        //    };

        //    _context.Orders.Add(order);
        //    await _context.SaveChangesAsync();

        //    var orderDto = _mapper.Map<PostOrderDto>(order);
        //    return Ok(orderDto);

            ////metoden legger checker ut kunden og innholdet i handlevognen blir lagret som en ordre. 
            ////Slette handlekurven etter den er lagret som ordre? i så fall ville leg lagt til denne funksjonen i denne metoden. 
          

            ////henter informasjon fra handlevogn
            //var cart = await _context.Carts
            //    .Include(c => c.Products)
            //    .FirstOrDefaultAsync(c => c.Id == postOrderDto.Id);

            ////oppretter en ny ordre:
            //var order = new Order { };
            //// oppretter en liste av produkter i order:
            //order.Products = new List<Product>();
            ////var order = new Order { Products = cart.Products, TotalPrice = cart.TotalPrice, DeliveryAdress = deliveryAdress };

            ////legger informasjon fra handlevogn og legger det til som et Order-objekt:

            ////var order = await _context.Orders.Include(o => o.Products)

            ////if (cart == null)
            ////{
            ////    return NotFound("Cart not found.");
            ////}

            


            ////cart.Order = order;
            //// _context.Orders.Add(order);
            ////await _context.SaveChangesAsync();

            ////return Ok("successfully checked out");
        // }


        

        //// DELETE: api/Orders/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOrder(int id)
        //{
        //    if (_context.Orders == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders.Include(o => o.Products).FirstOrDefaultAsync(c => c.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    var productsInOrder = _context.Products.Where(p => p.Id == order.Id);

        //    foreach (var product in productsInOrder)
        //    {
        //        product.OrderId = null;
        //    }

        //    _context.Orders.Remove(order);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool OrderExists(int id)
        //{
        //    return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
