using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : ControllerBase
    {
		private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;
		public BillController(IBillRepository billRepository, IMapper mapper)
        {
			_billRepository = billRepository;
            _mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBills()
		{
			var result = await _billRepository.GetAllBillsAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetBillByBillNumberAsync(string billNumber)
		{
			if (!_billRepository.BillExists(billNumber))
				return NotFound();

			var bill = await _billRepository.GetBillByBillNumberAsync(billNumber);

			return Ok(bill);
		}

		[HttpPost]
		public IActionResult AddBill(BillDto billDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var billMapped = _mapper.Map<Bill>(billDto);

			if (!_billRepository.AddBill(billMapped))
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

			if (!_billRepository.BillExists(billNumber))
				return NotFound("Bill does not exist");

			var billMapped = _mapper.Map<Bill>(billDto);

			if (!_billRepository.UpdateBill(billMapped))
				return BadRequest("Error while saving.");

			return StatusCode(200, "Successfully updated.");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteBill(string billNumber)
		{

			if (!_billRepository.BillExists(billNumber))
			{
				ModelState.AddModelError("", "Bill does not exist.");
				return BadRequest(ModelState);
			}
			var billToDelete = await _billRepository.GetBillByBillNumberAsync(billNumber);

			if (!_billRepository.DeleteBill(billToDelete))
			{
				ModelState.AddModelError("", "Something went wrong while saving.");
				return BadRequest(ModelState);
			}

			return Ok("Product deleted successfully.");
		}
	}
}
