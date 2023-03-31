using CashRegister.API.Dto;
using CashRegister.API.Mediator.Commands.ProductBillCommands;
using CashRegister.Application.Interfaces;
using CashRegister.Domain.Models;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.ProductBillHandlers
{
	public class AddProductToBillHandler : IRequestHandler<AddProductToBillCommand, bool>
	{
		private readonly IProductBillService _productBillService;

		public AddProductToBillHandler(IProductBillService productBillService)
		{
			_productBillService = productBillService;
		}

		public async Task<bool> Handle(AddProductToBillCommand request, CancellationToken cancellationToken)
		{
			if (_productBillService.ProductBillExists(request.ProductId, request.BillNumber))
				return false;

			return await _productBillService.AddProductToBill(request.ProductId, request.BillNumber, request.ProductBillDto);
		}
	}
}
