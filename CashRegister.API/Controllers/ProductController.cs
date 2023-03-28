using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _productRepository;

		private readonly IMapper _mapper;

		public ProductController(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
		{
			var result = await _productRepository.GetAllProductsAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductByIdAsync(int id)
		{
			var product = await _productRepository.GetProductByIdAsync(id);
			if (product == null)
				return NotFound("Product not found");

			return Ok(product);
		}

		[HttpPost]
		public IActionResult AddProduct(ProductDto productDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var productMapped = _mapper.Map<Product>(productDto);

			if (!_productRepository.AddProduct(productMapped))
			{
				ModelState.AddModelError("", "Error adding new product.");
				return BadRequest(ModelState);
			}

			return Ok("Product added successfully.");
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_productRepository.ProductExists(id))
				return NotFound("Product does not exist");

			var productMapped = _mapper.Map<Product>(productDto);

			if (!_productRepository.UpdateProduct(productMapped))
				return BadRequest("Error while saving.");

			return StatusCode(200, "Successfully updated.");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _productRepository.GetProductByIdAsync(id);
			if (product == null)
			{
				ModelState.AddModelError("", "Product does not exist.");
				return BadRequest(ModelState);
			}

			if (!_productRepository.DeleteProduct(product))
			{
				ModelState.AddModelError("", "Something went wrong while saving.");
				return BadRequest(ModelState);
			}

			return Ok("Product deleted successfully.");
		}
		
	}
}
