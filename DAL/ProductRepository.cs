using DAL.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

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


    }
}
