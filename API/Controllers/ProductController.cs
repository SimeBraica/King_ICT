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

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="logger">The logger instance.</param>
        public ProductController(HttpClient httpClient, IMemoryCache memoryCache, ILogger<ProductController> logger) {
            _memoryCache = memoryCache;
            _productService = new ProductService(httpClient);
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of products.</returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="400">If there is an error in the request.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <response code="404">If no products are found.</response>
        /// <response code="500">If there is an internal server error.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Product() {
            _logger.LogInformation("Getting all products");
            var products = await _productService.GetAllProducts();
            _logger.LogInformation("Successfully retrieved all products");
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        /// <response code="200">Returns the product.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
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


        /// <summary>
        /// Retrieves products by category and price.
        /// </summary>
        /// <param name="category">The category of the products.</param>
        /// <param name="price">The price of the products.</param>
        /// <returns>A list of products matching the specified category and price.</returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <response code="404">If no products are found.</response>
        /// <response code="500">If there is an internal server error.</response>
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

        /// <summary>
        /// Searches products by a search term.
        /// </summary>
        /// <param name="searchTerm">The search term to filter products.</param>
        /// <returns>A list of products matching the search term.</returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="400">If the request is invalid.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <response code="404">If no products are found.</response>
        /// <response code="500">If there is an internal server error.</response>
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
