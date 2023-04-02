using CashRegister.API.Dto;
using CashRegister.API.Mediator.Commands.ProductBillCommands;
using CashRegister.API.Mediator.Querries.ProductBillQuerries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductBillController : ControllerBase
    {
		private readonly IMediator _mediator;
		public ProductBillController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProductBills()
		{
			var querry = new GetAllProductBillsQuerry();
			var result = await _mediator.Send(querry);
			return result != null ? Ok(result) : BadRequest(ModelState);
		}

		[HttpPost]
        public async Task<IActionResult> AddProductToBill(int productId, string billNumber, ProductBillDto productBillDto)
        {
            if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var querry = new AddProductToBillCommand(productId, billNumber, productBillDto);
			var result = await _mediator.Send(querry);

			return result == true ? StatusCode(200, true) : BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductFromBill(int productId, string billNumber)
        {
			var querry = new DeleteProductFromBillCommand(billNumber, productId);
			var result = await _mediator.Send(querry);

			return result == true ? StatusCode(200, true) : BadRequest(ModelState);
        }

    }
}
