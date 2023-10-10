using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Data
{
    public class Cart
    {
        // Cart.cs represents the entity with properties in the database:
        // i use ? to make the property nullable
        public int Id { get; set; } //primary Key
        public int TotalPrice { get; set; } = 0;

        //Navigation properties?? 
        public virtual ICollection<Product>? Products { get; set; }

        [ForeignKey(nameof(OrderId))]
        public int? OrderId { get; set; } //foreignKey to Order
        public Order? Order { get; set; }
        
       
    }
}
