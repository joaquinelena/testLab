using JoaquinCustomer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoaquinCustomer.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
    }
}