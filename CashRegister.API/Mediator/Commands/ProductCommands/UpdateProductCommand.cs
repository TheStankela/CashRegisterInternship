using CashRegister.API.Dto;
using MediatR;

namespace CashRegister.API.Mediator.Commands.ProductCommands
{
	public class UpdateProductCommand : IRequest<bool>
	{
		public int ProductId { get; set; }
		public ProductDto ProductDto { get; set; }
		public UpdateProductCommand(int productId, ProductDto productDto)
		{
			ProductId = productId;
			ProductDto = productDto;
		}
	}
}
