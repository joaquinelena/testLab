using AutoMapper;
using JoaquinCustomer.DTOs;
using JoaquinCustomer.Models;
using JoaquinCustomer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoaquinCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await _service.GetAllCustomers();
            return Ok(_mapper.Map<IEnumerable<CustomerDTO>>(customers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            var customer = await _service.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return _mapper.Map<CustomerDTO>(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, UpdateCustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.Id = id;

            var updatedCustomer = await _service.UpdateCustomer(customer);
            return Ok(_mapper.Map<CustomerDTO>(updatedCustomer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CreateCustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.RegistrationDate = System.DateTime.Now;

            var newCustomer = await _service.AddCustomer(customer);
            var customerToReturn = _mapper.Map<CustomerDTO>(newCustomer);

            return CreatedAtAction("GetCustomer", new { id = customerToReturn.Id }, customerToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _service.DeleteCustomer(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}