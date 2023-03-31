using CashRegister.API.Mediator.Commands.ProductBillCommands;
using CashRegister.Application.Interfaces;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.ProductBillHandlers
{
	public class DeleteProductFromBillHandler : IRequestHandler<DeleteProductFromBillCommand, bool>
	{
		private readonly IProductBillService _productBillService;
		public DeleteProductFromBillHandler(IProductBillService productBillService)
		{
			_productBillService = productBillService;
		}
		public async Task<bool> Handle(DeleteProductFromBillCommand request, CancellationToken cancellationToken)
		{
			if (!_productBillService.ProductBillExists(request.ProductId, request.BillNumber))
				return false;

			var productBillToDelete = await _productBillService.GetProductBill(request.ProductId, request.BillNumber);

			return await _productBillService.DeleteProductFromBill(productBillToDelete);
		}
	}
}
