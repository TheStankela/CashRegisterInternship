using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.API.Mediator.Commands.ProductCommands;
using CashRegister.API.Mediator.Querries.ProductQuerries;
using CashRegister.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CashRegister.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IMediator _mediator;
		public ProductController(IProductService productService, IMediator mediator)
		{
			_productService = productService;
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
		{
			var querry = new GetAllProductsQuerry();
			var result = await _mediator.Send(querry);

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductByIdAsync(int id)
		{

			var querry = new GetProductByIdQuerry(id);
			var result = await _mediator.Send(querry);

			return result != null ? Ok(result) : NotFound("Product does not exist.");
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct(ProductDto productDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var querry = new AddProductCommand(productDto);
			var result = await _mediator.Send(querry);
			return result == true ? Ok("Successfully added.") : BadRequest(ModelState);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDto productDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var querry = new UpdateProductCommand(productId, productDto);
			var result = await _mediator.Send(querry);

			return result == true ? Ok("Updated successfully.") : BadRequest(ModelState);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var querry = new DeleteProductCommand(id);
			var result = await _mediator.Send(querry);

			return result == true ? Ok("Deleted successfully") : BadRequest(ModelState);
		}
		
	}
}
