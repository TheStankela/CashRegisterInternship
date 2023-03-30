using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Repositories;

namespace CashRegister.Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
		public ProductService(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public bool AddProduct(ProductDto productDto)
		{
			var productMapped = _mapper.Map<Product>(productDto);

			return _productRepository.AddProduct(productMapped);
		}

		public async Task<bool> DeleteProduct(int productId)
		{
			var productToDelete = await _productRepository.GetProductByIdAsync(productId);

			return _productRepository.DeleteProduct(productToDelete);
		}

		public async Task<List<ProductDto>> GetAllProductsAsync()
		{
			var products = await _productRepository.GetAllProductsAsync();

			var productsMapped = _mapper.Map<List<ProductDto>>(products);
			return productsMapped;
		}

		public async Task<ProductDto> GetProductByIdAsync(int productId)
		{
			var product = await _productRepository.GetProductByIdAsync(productId);

			var productMapped = _mapper.Map<ProductDto>(product);
			return productMapped;
		}

		public bool UpdateProduct(int productId, ProductDto productDto)
		{
			if (!_productRepository.ProductExists(productId))
				return false;

			var productMapped = _mapper.Map<Product>(productDto);
			productMapped.Id = productId;

			return _productRepository.UpdateProduct(productMapped);
		}
	}
}