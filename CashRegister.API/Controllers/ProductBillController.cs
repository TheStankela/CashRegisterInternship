using CashRegister.API.Dto;
using CashRegister.Application.Interfaces;
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

            if (_productBillService.ProductBillExists(productId, billNumber))
            {
                ModelState.AddModelError("", "Product is already on the bill.");
				return StatusCode(403, ModelState);
			}

			if (!await _productBillService.AddProductToBill(productId, billNumber, productBillDto))
                return BadRequest(ModelState);

			return Ok("Product added to bill successfully!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductFromBill(int productId, string billNumber)
        {
			if (!_productBillService.ProductBillExists(productId, billNumber))
			{
				ModelState.AddModelError("", "Product is not on the bill.");
				return StatusCode(403, ModelState);
			}

			var productBillToDelete = await _productBillService.GetProductBill(productId, billNumber);

			if (!await _productBillService.DeleteProductFromBill(productBillToDelete))
                return BadRequest(ModelState);

            return Ok("Successfully deleted");
        }

    }
}
