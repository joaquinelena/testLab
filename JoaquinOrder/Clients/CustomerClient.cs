using JoaquinOrder.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JoaquinOrder.Clients
{
    public class CustomerClient
    {
        private readonly HttpClient _httpClient;

        public CustomerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerDto> GetCustomerAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/Customers/{id}");
        }
    }
}