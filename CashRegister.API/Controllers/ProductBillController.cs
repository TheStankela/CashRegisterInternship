using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Application.Interfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductBillController : ControllerBase
    {
		private readonly IProductBillService _productBillService;

		public ProductBillController(IProductBillService productBillService)
        {
			_productBillService = productBillService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProductBills()
		{
			var result = await _productBillService.GetAllProductBills();

			return Ok(result);
		}

		[HttpPost]
        public async Task<IActionResult> AddProductToBill(int productId, string billNumber, ProductBillDto productBillDto)
        {
            if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _productBillService.AddProductToBill(productId, billNumber, productBillDto))
                return BadRequest(ModelState);

			return Ok("Product added to bill successfully!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductToBill(int productId, string billNumber)
        {
            if(!await _productBillService.DeleteProductFromBill(productId, billNumber))
                return BadRequest();

            return Ok("Successfully deleted");
        }

    }
}
