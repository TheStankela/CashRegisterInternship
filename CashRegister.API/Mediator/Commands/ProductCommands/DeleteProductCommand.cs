using CashRegister.API.Dto;
using MediatR;

namespace CashRegister.API.Mediator.Commands.ProductCommands
{
	public class DeleteProductCommand : IRequest<bool>
	{
		public int ProductId { get; set; }
		public DeleteProductCommand(int productId)
		{
			ProductId = productId;
		}
	}
}
