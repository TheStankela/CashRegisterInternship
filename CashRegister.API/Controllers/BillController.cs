using CashRegister.API.Dto;
using CashRegister.API.Mediator.Commands;
using CashRegister.API.Mediator.Commands.BillCommands;
using CashRegister.API.Mediator.Querries;
using CashRegister.API.Mediator.Querries.BillQuerries;
using CashRegister.Application.Interfaces;
using CashRegister.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BillController : ControllerBase
	{
		private readonly IMediator _mediator;
		public BillController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBills()
		{
			var querry = new GetAllBillsQuerry();
			var result = await _mediator.Send(querry);
			return Ok(result);
		}

		[HttpGet("{billNumber}")]
		public async Task<IActionResult> GetBillByBillNumberAsync(string billNumber)
		{
			var querry = new GetBillByNumberQuerry(billNumber);
			var result = await _mediator.Send(querry);
			return result != null ? Ok(result) : NotFound("Bill does not exist");
		}

		[HttpGet("{billNumber}/{currency}")]
		public async Task<IActionResult> GetBillWithUpdatedCurrency(string billNumber, string currency)
		{
			var querry = new GetBillWithUpdatedCurrencyQuerry(billNumber, currency);
			var result = await _mediator.Send(querry);
			return result != null ? Ok(result) : NotFound("Bill does not exist");
		}

		[HttpPost]
		public async Task<IActionResult> AddBill(AddBillDto billDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var querry = new CreateBillCommand(billDto);
			var result = await _mediator.Send(querry);

			return result == true ? Ok("Added successfully.") : BadRequest(ModelState);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateBill(string billNumber, [FromBody] AddBillDto billDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var querry = new UpdateBillCommand(billDto, billNumber);
			var result = await _mediator.Send(querry);

			return result == true ? Ok("Updated successfully.") : BadRequest(ModelState);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteBill(string billNumber)
		{
			var querry = new DeleteBillCommand(billNumber);
			var result = await _mediator.Send(querry);

			return result == true ? StatusCode(200, result) : NotFound("Bill does not exist.");
		}
	}
}
