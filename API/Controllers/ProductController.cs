using BAL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {



        private readonly ProductService _productService;

        public ProductController(HttpClient httpClient) {
            _productService = new ProductService(httpClient);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Product() {
            var products = await _productService.GetAllProducts();
            return products.ToList();

        }
    }
}
