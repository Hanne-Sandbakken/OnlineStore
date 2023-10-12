using OnlineStore.Dto.Cart;
using OnlineStore.Dto.Product;

namespace OnlineStore.Dto.Order
{
    public class PostCartToOrderDto
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public List<GetProductDto> Products { get; set; }
    }
}
