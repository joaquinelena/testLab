using BlazorClient.DTOs;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClient.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "http://localhost:5002/api/Orders";

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrderDTO>> GetOrdersAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<OrderDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<OrderDTO>> GetOrdersByCustomerAsync(int customerId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/customer/{customerId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<OrderDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderDTO order)
        {
            var json = JsonSerializer.Serialize(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OrderDTO>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}