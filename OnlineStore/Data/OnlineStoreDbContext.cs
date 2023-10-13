using Microsoft.EntityFrameworkCore;
namespace OnlineStore.Data
{
    public class OnlineStoreDbContext : DbContext
    {
        //OnllineStoreDbContext.cs interact with the database using Entity Framework.
        public OnlineStoreDbContext(DbContextOptions options ) : base(options)
        {

            
        }

        //Creates a list in the database of type Product, Cart and Order.
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        //Seed data to database. Products the user can search through:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Bukett vase klar", Description = "Bukett vase klar 210 mm", Price = 2649, ImageUrl = "https://www.magnor.no/wp-content/uploads/2020/10/8712012-Bukett-Vase-332x530.png" },
                new Product { Id = 2, Name = "Family vase brun medium", Description = "Family vase brun medium, 17 cm", Price = 1299, ImageUrl = "https://www.magnor.no/wp-content/uploads/2023/06/209021-406x530.jpg" },
                new Product { Id = 3, Name = "Family vase brun stor", Description = "Family vase brun stor, 26 cm", Price = 1799, ImageUrl = "https://www.magnor.no/wp-content/uploads/2023/06/209031-426x530.jpg" },
                new Product { Id = 4, Name = "Family vase klar liten", Description = "Family vase klar liten 11 cm", Price = 629, ImageUrl = "https://www.magnor.no/wp-content/uploads/2023/01/Galaxie_08-420x530.jpg" },
                new Product { Id = 5, Name = "Family vase klar medium", Description = "Family vase klar medium 17 cm", Price = 1049, ImageUrl = "https://www.magnor.no/wp-content/uploads/2023/01/209020-1-433x530.jpg" },
                new Product { Id = 6, Name = "Family vase klar stor", Description = "Family vase klar stor 26 cm", Price = 1579, ImageUrl = "https://www.magnor.no/wp-content/uploads/2023/01/209030-1-436x530.jpg" },
                new Product { Id = 7, Name = "Galaxie lykt/vase koks liten", Description = "Galaxie lykt/vase koks liten 8,5 cm", Price = 529, ImageUrl = "https://www.magnor.no/wp-content/uploads/2023/01/220711-507x530.jpg" },
                new Product { Id = 8, Name = "Galaxie lykt/vase koks medium", Description = "Galaxie lykt/vase koks medium 16,5 cm", Price = 849, ImageUrl = "https://www.magnor.no/wp-content/uploads/2023/01/220721-489x530.jpg" },
                new Product { Id = 9, Name = "Galaxie lykt/vase koks stor", Description = "Galaxie lykt/vase koks stor 23 cm", Price = 1149, ImageUrl = "https://www.magnor.no/wp-content/uploads/2023/01/220731-497x530.jpg" },
                new Product { Id = 10, Name = "Iglo telykt", Description = "Liten varm cognac 90 mm", Price = 629, ImageUrl = "https://www.magnor.no/wp-content/uploads/2021/01/Iglo_07-460x530.jpg" },
                new Product { Id = 11, Name = "Iglo stormlykt/vase", Description = "Stor varm cognac 220 mm", Price = 1569, ImageUrl = "https://www.magnor.no/wp-content/uploads/2021/01/Iglo_06-353x530.jpg" }
                ); 
        }
    }
}
