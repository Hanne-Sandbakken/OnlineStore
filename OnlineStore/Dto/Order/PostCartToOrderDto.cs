using OnlineStore.Dto.Cart;
using OnlineStore.Dto.Product;

namespace OnlineStore.Dto.Order
{
    public class PostCartToOrderDto
    {
        //Dto used in PostOrder() in OrderController. CartId to get the information i need from cart, and adding delivery address.
        //The user doesnt need to have access to the whole object when checking out. 
        public int CartId { get; set; }
        public string DeliveryAdress { get; set; }
    }
}
