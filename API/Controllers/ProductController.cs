using BAL;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using static System.Net.WebRequestMethods;

namespace API.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {

        private IMemoryCache iMemory;
        private readonly ProductService _productService;

        public ProductController(HttpClient httpClient, IMemoryCache imemory) {
            iMemory = imemory;
            _productService = new ProductService(httpClient);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Product() {
            
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductById(int id) {
            Cache _cache = new Cache(iMemory);
            var cacheKey = $"product_{id}";
            var product = _cache.GetFromCache(cacheKey);
            if (product == null) {
                product = await _productService.GetProduct(id);
                _cache.AddToCache(cacheKey, product);
            }
            return Ok(product);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductWithFilters([FromQuery] string category, [FromQuery] decimal price) {
            Cache _cache = new Cache(iMemory);
            var cacheKey = $"product_{category}_{price}";
            var products = _cache.GetFromCache(cacheKey);
            if (products == null) {
                products = await _productService.FilterProducts(category, price);
                _cache.AddToCache(cacheKey, products);
            }
            return Ok(products);
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string searchTerm) {
            Cache _cache = new Cache(iMemory);
            var cacheKey = $"product_{searchTerm}";
            var products = _cache.GetFromCache(cacheKey);
            if (products == null) {
                products = await _productService.SearchProducts(searchTerm.ToLower());
                _cache.AddToCache(cacheKey, products);
            }
            return Ok(products);
        }


    }
}
