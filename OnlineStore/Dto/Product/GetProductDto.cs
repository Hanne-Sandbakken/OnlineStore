namespace OnlineStore.Dto.Product
{
    public class GetProductDto
    {
        //Dto used in ProductsController: GetProducts() and GetProduct(id)
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
