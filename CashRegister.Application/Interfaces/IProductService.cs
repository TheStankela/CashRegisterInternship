using CashRegister.API.Dto;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Services
{
	public interface IProductService
	{
		bool AddProduct(ProductDto productDto);
		Task<bool> DeleteProduct(int productId);
		Task<List<Product>> GetAllProductsAsync();
		Task<Product> GetProductByIdAsync(int productId);
		bool UpdateProduct(int productId, ProductDto productDto);
	}
}