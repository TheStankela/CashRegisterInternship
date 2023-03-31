using CashRegister.API.Dto;
using MediatR;

namespace CashRegister.API.Mediator.Querries.ProductQuerries
{
	public class GetProductByIdQuerry : IRequest<ProductDto>
	{
		public int Id { get; set; }

		public GetProductByIdQuerry(int id)
		{
			Id = id;
		}
	}
}
