using JoaquinOrder.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JoaquinOrder.Clients
{
    public class ProductClient
    {
        private readonly HttpClient _httpClient;

        public ProductClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto> GetProductAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/Products/{id}");
        }

        public async Task<bool> UpdateProductStockAsync(int id, int newStock)
        {
            var product = await GetProductAsync(id);
            if (product == null)
                return false;

            product.Stock = newStock;
            var response = await _httpClient.PutAsJsonAsync($"api/Products/{id}", product);
            return response.IsSuccessStatusCode;
        }
    }
}