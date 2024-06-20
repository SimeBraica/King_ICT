using BAL;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static System.Net.WebRequestMethods;

namespace API.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {


        private Cache cache = new Cache();
        private readonly ProductService _productService;

        public ProductController(HttpClient httpClient) {
            _productService = new ProductService(httpClient);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Product() {
            
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductByTitle(int id) {
            var product = await _productService.GetProduct(id);
            return Ok(product);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductWithFilters([FromQuery] string category, [FromQuery] decimal price) {
            var products = await _productService.FilterProducts(category, price);
            return Ok(products);
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string searchTerm) {
            var products = await _productService.SearchProducts(searchTerm.ToLower());
            return Ok(products);
        }


    }
}
