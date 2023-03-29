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

		public Task<List<Product>> GetAllProductsAsync()
		{
			return _productRepository.GetAllProductsAsync();
		}

		public Task<Product> GetProductByIdAsync(int productId)
		{
			return _productRepository.GetProductByIdAsync(productId);
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