using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Data
{
    public class Order
    {
        // Order.cs  represents the entity with properties in the database. 
        // i use ? to make the property nullable
        // This model represent information needed to checkout an order, with information about products ordered, total price, and Delivery Adress.  
        public int Id { get; set; } //Primary Key
        public int TotalPrice { get; set; }
        public string DeliveryAdress { get; set; }

        //navigation Properties:
        public virtual ICollection<Product> Products { get; set; }


    }
}
