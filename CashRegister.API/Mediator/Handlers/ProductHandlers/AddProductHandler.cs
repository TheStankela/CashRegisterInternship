using CashRegister.API.Dto;
using CashRegister.API.Mediator.Commands.ProductCommands;
using CashRegister.Application.Services;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.ProductHandlers
{
	public class AddProductHandler : IRequestHandler<AddProductCommand, bool>
	{
		private readonly IProductService _productService;
		public AddProductHandler(IProductService productService)
		{
			_productService = productService;
		}
		public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
		{
			return _productService.AddProduct(request.productDto);
		}
	}
}
