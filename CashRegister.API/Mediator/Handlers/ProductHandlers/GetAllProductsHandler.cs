using CashRegister.API.Dto;
using CashRegister.API.Mediator.Querries.ProductQuerries;
using CashRegister.Application.Services;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.ProductHandlers
{
	public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuerry, List<ProductDto>>
	{
		private readonly IProductService _productService;

		public GetAllProductsHandler(IProductService productService)
		{
			_productService = productService;
		}

		public async Task<List<ProductDto>> Handle(GetAllProductsQuerry request, CancellationToken cancellationToken)
		{
			return await _productService.GetAllProductsAsync();
		}
	}
}
