using CashRegister.API.Mediator.Commands.ProductCommands;
using CashRegister.Application.Services;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.ProductHandlers
{
	public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
	{
		private readonly IProductService _productService;
		public DeleteProductHandler(IProductService productService)
		{
			_productService = productService;
		}
		public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
		{
			return await _productService.DeleteProduct(request.ProductId);
		}
	}
}
