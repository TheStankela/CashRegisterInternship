using CashRegister.API.Dto;
using CashRegister.Application.Interfaces;
using CashRegister.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BillController : ControllerBase
	{
		private readonly IBillService _billService;
		private readonly IValidationService _validationService;
		public BillController(IBillService billService, IValidationService validationService)
		{
			_billService = billService;
			_validationService = validationService;
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
			if (!_billService.BillExists(billNumber))
				return NotFound("Bill does not exist");

			var bill = await _billService.GetBillByBillNumberAsync(billNumber);
			return Ok(bill);
		}

		[HttpGet("{billNumber}/{currency}")]
		public async Task<IActionResult> GetBillWithUpdatedCurrency(string billNumber, string currency)
		{
			if (!_billService.BillExists(billNumber))
				return NotFound("Bill does not exist");

			var billToGet = await _billService.GetBillByBillNumberAsync(billNumber);

			var updatedBill = await _billService.DisplayBill(billToGet, currency);
			return Ok(updatedBill);
		}

		[HttpPost]
		public IActionResult AddBill(AddBillDto billDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_validationService.ValidateBill(billDto))
			{
				ModelState.AddModelError("", "One or more validation errors occured.");
				return StatusCode(400, ModelState);
			}

			if (!_billService.AddBill(billDto))
			{
				ModelState.AddModelError("", "Error adding new bill.");
				return StatusCode(500, ModelState);
			}

			return Ok("Bill created successfully.");
		}

		[HttpPut]
		public IActionResult UpdateBill(string billNumber, [FromBody] AddBillDto billDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!_billService.BillExists(billNumber))
				return NotFound("Bill does not exist.");

			if (!_billService.UpdateBill(billNumber, billDto))
				return BadRequest("Error while saving.");

			return StatusCode(200, "Successfully updated.");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteBill(string billNumber)
		{
			if (!_billService.BillExists(billNumber))
				return NotFound("Bill not found.");

			var billToDelete = await _billService.GetBillByBillNumberAsNoTracking(billNumber);

			if (!_billService.DeleteBill(billToDelete))
			{
				ModelState.AddModelError("", "Something went wrong while saving.");
				return BadRequest(ModelState);
			}

			return Ok("Bill deleted successfully.");
		}
	}
}
