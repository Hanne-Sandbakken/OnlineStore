using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Data
{
    public class Product
    {
        // Product.cs  represents the entity with properties in the database:
        // i use ? to make the property nullable
        public int Id { get; set; } //Primary Key, Id property needed for GET /api/products/{id} method. 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }

        //Navigation Properties:
        [ForeignKey(nameof(CartId))]
        public int? CartId { get; set; } //Foreign Key to Cart
        public Cart? Cart { get; set; }

        [ForeignKey(nameof(OrderId))]
        public int? OrderId { get; set; } //foreign Key to Order
        public Order? Order { get; set; }
    }
}
