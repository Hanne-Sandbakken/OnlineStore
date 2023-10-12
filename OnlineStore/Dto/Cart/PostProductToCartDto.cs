using OnlineStore.Dto.Product;

namespace OnlineStore.Dto.Cart
{
    public class PostProductToCartDto
    {
        public int ProductId { get; set; }
        public int TotalPrice { get; set; }
        public List<GetProductDto> Products { get; set; }
    }
}
