using BAL;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers {
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase {

        private readonly IMemoryCache _memoryCache;
        private readonly ProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(HttpClient httpClient, IMemoryCache memoryCache, ILogger<ProductController> logger) {
            _memoryCache = memoryCache;
            _productService = new ProductService(httpClient);
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Product() {
            _logger.LogInformation("Getting all products");
            var products = await _productService.GetAllProducts();
            _logger.LogInformation("Successfully retrieved all products");
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> ProductById(int id) {
            _logger.LogInformation($"Getting product by id: {id}");
            Cache _cache = new Cache(_memoryCache);
            var cacheKey = $"product_{id}";
            var product = _cache.GetFromCache(cacheKey);
            if (product == null) {
                _logger.LogInformation($"Product not found in cache for id: {id}, fetching from service");
                product = await _productService.GetProduct(id);
                _cache.AddToCache(cacheKey, product);
            } else {
                _logger.LogInformation($"Product found in cache for id: {id}");
            }
            return Ok(product);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductWithFilters([FromQuery] string category, [FromQuery] decimal price) {
            _logger.LogInformation($"Getting products by category: {category} and price: {price}");
            Cache _cache = new Cache(_memoryCache);
            var cacheKey = $"product_{category}_{price}";
            var products = _cache.GetFromCache(cacheKey);
            if (products == null) {
                _logger.LogInformation($"Products not found in cache for category: {category} and price: {price}, fetching from service");
                products = await _productService.FilterProducts(category, price);
                _cache.AddToCache(cacheKey, products);
            } else {
                _logger.LogInformation($"Products found in cache for category: {category} and price: {price}");
            }
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string searchTerm) {
            _logger.LogInformation($"Searching products with term: {searchTerm}");
            Cache _cache = new Cache(_memoryCache);
            var cacheKey = $"product_{searchTerm}";
            var products = _cache.GetFromCache(cacheKey);
            if (products == null) {
                _logger.LogInformation($"Products not found in cache for searchTerm: {searchTerm}, fetching from service");
                products = await _productService.SearchProducts(searchTerm.ToLower());
                _cache.AddToCache(cacheKey, products);
            } else {
                _logger.LogInformation($"Products found in cache for searchTerm: {searchTerm}");
            }
            return Ok(products);
        }
    }
}
