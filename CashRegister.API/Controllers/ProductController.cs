using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
		{
			var result = await _productService.GetAllProductsAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductByIdAsync(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);
			if (product == null)
				return NotFound("Product not found");

			return Ok(product);
		}

		[HttpPost]
		public IActionResult AddProduct(ProductDto productDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_productService.AddProduct(productDto))
			{
				ModelState.AddModelError("", "Error adding new product.");
				return BadRequest(ModelState);
			}

			return Ok("Product added successfully.");
		}

		[HttpPut]
		public IActionResult UpdateProduct(int productId, [FromBody] ProductDto productDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_productService.UpdateProduct(productId, productDto))
				return BadRequest("Error while saving.");

			return StatusCode(200, "Successfully updated.");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			if (!await _productService.DeleteProduct(id))
			{
				ModelState.AddModelError("", "Something went wrong while saving.");
				return BadRequest(ModelState);
			}

			return Ok("Product deleted successfully.");
		}
		
	}
}
