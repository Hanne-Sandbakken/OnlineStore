using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Data
{
    public class Order
    {
        // Order.cs  represents the entity with properties in the database
        // i use ? to make the property nullable
        public int Id { get; set; } //Primary Key
        public int TotalPrice { get; set; }
        public string DeliveryAdress { get; set; }

        //navigation Properties:
        public virtual ICollection<Product> Products { get; set; }

        //1-1-forhold med Cart. Trenger ikke spesifisere FK her siden forholdet er definert i Cart.cs
        //[ForeignKey(nameof(CartId))]
        //public int CartId { get; set; } //foreign key to Cart
        //public required Cart Cart { get; set; }


    }
}
