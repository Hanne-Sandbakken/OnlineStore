using OnlineStore.Dto.Product;

namespace OnlineStore.Dto.Cart
{
    public class GetCartDto
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public List<GetProductDto> Products { get; set; }
    }
}
