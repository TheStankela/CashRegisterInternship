using CashRegister.Domain.Models;

namespace CashRegister.Infrastructure.Repositories
{
	public interface IProductRepository
	{
		public bool AddProduct(Product product);
		public Task<Product> GetProductByIdAsync(int id);
		public Task<List<Product>> GetAllProductsAsync();
		public bool Save();
		public bool DeleteProduct(Product product);
		public bool UpdateProduct(Product product);
		public bool ProductExists(int id);
	}
}