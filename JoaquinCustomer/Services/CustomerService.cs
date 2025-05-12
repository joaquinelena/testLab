using JoaquinCustomer.Models;
using JoaquinCustomer.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoaquinCustomer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _repository.GetAllCustomers();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _repository.GetCustomerById(id);
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            return await _repository.AddCustomer(customer);
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            return await _repository.UpdateCustomer(customer);
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            return await _repository.DeleteCustomer(id);
        }
    }
}