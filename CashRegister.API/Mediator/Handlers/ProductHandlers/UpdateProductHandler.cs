using CashRegister.API.Mediator.Commands.ProductCommands;
using CashRegister.Application.Services;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.ProductHandlers
{
	public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
	{
		private readonly IProductService _productService;
		public UpdateProductHandler(IProductService productService)
		{
			_productService = productService;
		}
		public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			return _productService.UpdateProduct(request.ProductId, request.ProductDto);
		}
	}
}
