using JoaquinProducts.Models;
using JoaquinProducts.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoaquinProducts.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _repository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _repository.GetProductById(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            return await _repository.AddProduct(product);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            return await _repository.UpdateProduct(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _repository.DeleteProduct(id);
        }
    }
}