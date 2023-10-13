using OnlineStore.Dto.Product;

namespace OnlineStore.Dto.Cart
{
    public class GetCartDto
    {
        //Data-transfer-object used in GetCart(); in CartsController: 
        public int Id { get; set; } 
        public int TotalPrice { get; set; }
        public List<GetProductDto> Products { get; set; }
    }
}
