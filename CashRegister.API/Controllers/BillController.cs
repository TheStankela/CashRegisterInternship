using CashRegister.API.Dto;
using CashRegister.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : ControllerBase
    {
		private readonly IBillService _billService;
		public BillController(IBillService billService)
        {
			_billService = billService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBills()
		{
			var result = await _billService.GetAllBillsAsync();

			return Ok(result);
		}

		[HttpGet("{billNumber}")]
		public async Task<IActionResult> GetBillByBillNumberAsync(string billNumber)
		{
			var bill = await _billService.GetBillByBillNumberAsync(billNumber);

			return Ok(bill);
		}

		[HttpPost]
		public async Task<IActionResult> AddBill(BillDto billDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _billService.AddBill(billDto))
			{
				ModelState.AddModelError("", "Error adding new bill.");
				return BadRequest(ModelState);
			}

			return Ok("Product added successfully.");
		}

		[HttpPut]
		public IActionResult UpdateBill(string billNumber, [FromBody] BillDto billDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_billService.UpdateBill(billNumber, billDto))
				return BadRequest("Error while saving.");

			return StatusCode(200, "Successfully updated.");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteBill(string billNumber)
		{
			if (!await _billService.DeleteBill(billNumber))
			{
				ModelState.AddModelError("", "Something went wrong while saving.");
				return BadRequest(ModelState);
			}

			return Ok("Product deleted successfully.");
		}
	}
}
