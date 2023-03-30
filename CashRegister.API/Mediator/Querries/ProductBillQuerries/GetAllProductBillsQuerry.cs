using CashRegister.Domain.Models;
using MediatR;

namespace CashRegister.API.Mediator.Querries.ProductBillQuerries
{
	public class GetAllProductBillsQuerry : IRequest<List<ProductBill>>
	{
		
	}
}
