using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly CashRegisterDBContext _context;

		public ProductRepository(CashRegisterDBContext context)
		{
			_context = context;
		}
		public bool AddProduct(Product product)
		{
			_context.Products.Add(product);
			return Save();
		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
		public async Task<List<Product>> GetAllProductsAsync()
		{
			return await _context.Products.ToListAsync();
		}
		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
		}

		public bool DeleteProduct(Product product)
		{
			_context.Remove(product);
			return Save();
		}

		public bool UpdateProduct(Product product)
		{
			_context.Update(product);
			return Save();
		}

		public bool ProductExists(int id)
		{
			return _context.Products.Any(x => x.Id == id);
		}
	}
}
