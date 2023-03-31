using CashRegister.API.Dto;
using CashRegister.API.Mediator.Querries.ProductQuerries;
using CashRegister.Application.Services;
using MediatR;

namespace CashRegister.API.Mediator.Handlers.ProductHandlers
{
	public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuerry, ProductDto>
	{
		private readonly IProductService _productService;
		public GetProductByIdHandler(IProductService productService)
		{
			_productService = productService;
		}
		public async Task<ProductDto> Handle(GetProductByIdQuerry request, CancellationToken cancellationToken)
		{
			return await _productService.GetProductByIdAsync(request.Id);
		}
	}
}
