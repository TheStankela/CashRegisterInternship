using CashRegister.API.Dto;
using MediatR;

namespace CashRegister.API.Mediator.Querries.ProductQuerries
{
	public class GetAllProductsQuerry : IRequest<List<ProductDto>>
	{
	}
}
