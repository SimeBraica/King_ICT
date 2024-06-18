using DAL.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
namespace DAL {
    public class ProductRepository : IDisposable {

        private readonly HttpClient? _httpClient;

        public ProductRepository(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public void Dispose() {
            _httpClient.Dispose();
        }
        public async Task<List<Product>> GetProductsAsync() {
            var productResponse = await _httpClient.GetFromJsonAsync<ProductResponse>("https://dummyjson.com/products");
            return productResponse.Products;
        }

        public async Task<List<Product>> GetProductByIdAsync(int id) {
            var productResponse = await _httpClient.GetFromJsonAsync<ProductResponse>($"https://dummyjson.com/products");
            return productResponse.Products.Where(p => p.Id == id).ToList();
        }
        
        public async Task<List<Product>> GetProductWithFiltersAsync(string category, decimal price) {
            var productResponse = await _httpClient.GetFromJsonAsync<ProductResponse>($"https://dummyjson.com/products");
            return productResponse.Products.Where(p => p.Category == category && p.Price == price).ToList();
        }

        public async Task<List<Product>> GetProductWithSearchAsync(string searchTerm) {
            var productResponse = await _httpClient.GetFromJsonAsync<ProductResponse>($"https://dummyjson.com/products");
            return productResponse.Products.Where(p => p.Title.ToLower().Contains(searchTerm)).ToList();
        }
    }
}
