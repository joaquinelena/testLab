using AutoMapper;
using JoaquinProducts.DTOs;
using JoaquinProducts.Models;
using JoaquinProducts.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoaquinProducts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _service.GetAllProducts();
            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _service.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductDTO>(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.Id = id;

            var updatedProduct = await _service.UpdateProduct(product);
            return Ok(_mapper.Map<ProductDTO>(updatedProduct));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(CreateProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var newProduct = await _service.AddProduct(product);
            var productToReturn = _mapper.Map<ProductDTO>(newProduct);

            return CreatedAtAction("GetProduct", new { id = productToReturn.Id }, productToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _service.DeleteProduct(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}