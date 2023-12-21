using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OnlineStore.Data;
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
        private readonly ILogger<ProductsController> _logger;

        //I inject the IMapper interface, so that i can use the AutoMapperConfig file and Dto-Classes
        public ProductsController(IMapper mapper, IProductsRepository productsRepository, ILogger<ProductsController> logger)
        {
            _mapper = mapper;
            _productsRepository = productsRepository;
            _logger = logger;
        }

        // GET: api/Products/GetAll
        //Finds all the products in the database and returns it as a list
        ///api/products/GetAll?$Select=name
        [HttpGet]
        [Route("api/products/GetAll")]
        [EnableQuery] //use OData do do query
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetProducts()
        {
            try
            {
                //gets the data from database and save to list. mapping is done in GetAllAsync<TResult>():
                var products = await _productsRepository.GetAllAsync<GetProductDto>();

                //returns the list:
                return Ok(products);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when getting products: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
       
        }

        //GET: api/Products/?StartIndex=0&pagesize=25&PageNumber=1
        //[FromQuery] means that im specifying that when you are sending over the query, I expect your url, i expect it to look like i decided it need to look like: 
        //Added pagination to GetPagedProducts. 
        [HttpGet]
        [Route("api/products/")]
        public async Task<ActionResult<PagedResult<GetProductDto>>> GetPagedProducts([FromQuery]QueryParameters queryParameters)
        {
            //gets the data from database, save it to the list Products, and do the mapping:
            var pagedProductsResult = await _productsRepository.GetAllAsync<GetProductDto>(queryParameters);
            //returns the list of dtos
            return Ok(pagedProductsResult);

        }

        // GET: api/Products/5
        // Finds one spesific product with id
        [HttpGet]
        [Route("api/products/{id}")]

        public async Task<ActionResult<GetProductDto>> GetProduct(int id)
        {
            var product = await _productsRepository.GetAsync(id);
            return Ok(product);

        }

    }
}
