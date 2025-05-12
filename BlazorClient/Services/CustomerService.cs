using BlazorClient.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClient.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "http://localhost:5001/api/Customers";

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CustomerDTO>> GetCustomersAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CustomerDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<CustomerDTO> CreateCustomerAsync(CustomerDTO customer)
        {
            var json = JsonSerializer.Serialize(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CustomerDTO>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}