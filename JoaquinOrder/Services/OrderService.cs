using JoaquinOrder.Clients;
using JoaquinOrder.Models;
using JoaquinOrder.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoaquinOrder.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ProductClient _productClient;
        private readonly CustomerClient _customerClient;

        public OrderService(
            IOrderRepository repository,
            ProductClient productClient,
            CustomerClient customerClient)
        {
            _repository = repository;
            _productClient = productClient;
            _customerClient = customerClient;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _repository.GetAllOrdersAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _repository.GetOrdersByCustomerIdAsync(customerId);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _repository.GetOrderByIdAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            decimal totalAmount = 0;

            var customer = await _customerClient.GetCustomerAsync(order.CustomerId);
            if (customer == null)
            {
                throw new Exception($"Customer with ID {order.CustomerId} not found");
            }

            order.CustomerName = customer.Name;

            foreach (var item in order.Items)
            {
                var product = await _productClient.GetProductAsync(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductId} not found");
                }

                if (product.Stock < item.Quantity)
                {
                    item.Quantity = product.Stock;
                }

                item.ProductName = product.Name;
                item.UnitPrice = product.Price;
                item.Subtotal = item.UnitPrice * item.Quantity;

                totalAmount += item.Subtotal;

                int newStock = product.Stock - item.Quantity;
                await _productClient.UpdateProductStockAsync(item.ProductId, newStock);
            }

            order.TotalAmount = totalAmount;
            order.OrderDate = DateTime.Now;

            return await _repository.CreateOrderAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _repository.DeleteOrderAsync(id);
        }
    }
}