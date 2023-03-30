using CashRegister.API.Dto;
using MediatR;

namespace CashRegister.API.Mediator.Commands.ProductCommands
{
	public class AddProductCommand : IRequest<bool>
	{
		public ProductDto productDto { get; set; }
		public AddProductCommand(ProductDto productDto)
		{
			this.productDto = productDto;
		}
	}
}
