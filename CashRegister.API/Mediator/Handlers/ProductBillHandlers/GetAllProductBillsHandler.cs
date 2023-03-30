using CashRegister.API.Mediator.Querries.ProductBillQuerries;
using CashRegister.Application.Interfaces;
using CashRegister.Domain.Models;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.ProductBillHandlers
{
	public class GetAllProductBillsHandler : IRequestHandler<GetAllProductBillsQuerry, List<ProductBill>>
	{
		private readonly IProductBillService _productBillService;

		public GetAllProductBillsHandler(IProductBillService productBillService)
        {
			_productBillService = productBillService;
		}
        public async Task<List<ProductBill>> Handle(GetAllProductBillsQuerry request, CancellationToken cancellationToken)
		{
			return await _productBillService.GetAllProductBills();
		}
	}
}
