using OnlineStore.Dto.Cart;
using OnlineStore.Dto.Product;

namespace OnlineStore.Dto.Order
{
    public class PostCartToOrderDto
    {
        public int CartId { get; set; }
        public string DeliveryAdress { get; set; }
    }
}
