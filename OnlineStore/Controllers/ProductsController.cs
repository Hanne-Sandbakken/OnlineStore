using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Dto.Product;
using OnlineStore.IRepository;

namespace OnlineStore.Controllers
{
    //I define the route at each endpoints of the controller. This can be beneficial if we decide to refactore the name of controller. The routing will not be affected, and the Api-users will not be affected by the name-change. 
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Controller accepts and processes HTTP requests, orchestrating the application's response. 
        //The database should be distracted from the controller, and the endpoints should not contain much logic.
        //I have therefor used Repository that handle the logic, and the contoller should redirect to find the logic in IRepository and Repository.
        //IMapper is used to map dto to models. It translate the dto to Product-object. I use different Dto-klasses for different endpoints, depending on what information of the object it need to do the job, or what information i want to give the user. 


        private readonly IMapper _mapper; 
        private readonly IProductsRepository _productsRepository;

        //I inject the IMapper interface, so that i can use the AutoMapperConfig file and Dto-Classes
        public ProductsController(IMapper mapper, IProductsRepository productsRepository)
        {
            _mapper = mapper;
            _productsRepository = productsRepository;
        }

        // GET: api/Products
        //Finds all the products in the database and returns it as a list
        [HttpGet]
        [Route("api/products")]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetProducts()
        {
            //gets the data from database, save it to the list Products:
            var products = await _productsRepository.GetAllAsync();
        
          //maps the information we saved in var products to an list of dtos. 
          var productDtos = _mapper.Map<List<GetProductDto>>(products);
            
          //returns the list of dtos
          return Ok(productDtos);
        }

        // GET: api/Products/5
        // Finds one spesific product with id
        [HttpGet]
        [Route("api/products/{id}")]

        public async Task<ActionResult<GetProductDto>> GetProduct(int id)
        {
            //Gets one product from database with id. Saves it in the variable product
            var product = await _productsRepository.GetAsync(id);

            // if it wasn't any data that got saved in product, return Notfound.
            if (product == null)
            {
                return NotFound();
            }

            //maps the product to Dto
            var productDto = _mapper.Map<GetProductDto>(product);

            //returns the dto: 
            return Ok(productDto);
        }

    }
}
