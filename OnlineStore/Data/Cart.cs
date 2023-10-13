using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Data
{
    public class Cart
    {
        // Cart.cs represents the entity with properties in the database
        // Id is used as primary Key, Total Price is important to show to custumers as what the total price in Cart is.. 
        // Products is a list of product-objects. This allows us to see the content of the cart.
        // i use ? to make the property nullable
        public int Id { get; set; } //primary Key
        public int TotalPrice { get; set; } = 0;

        //Navigation properties
        public virtual ICollection<Product>? Products { get; set; }

        [ForeignKey(nameof(OrderId))]
        public int? OrderId { get; set; } //foreignKey to Order
        public Order? Order { get; set; }
        
       
    }
}
