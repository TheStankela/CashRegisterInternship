using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Infrastructure.Repositories
{
	public class ProductBillRepository : IProductBillRepository
	{
		private readonly CashRegisterDBContext _context;

		public ProductBillRepository(CashRegisterDBContext context)
		{
			_context = context;
		}
		public async Task<List<ProductBill>> GetAllProductBills()
		{
			return await _context.ProductBills.ToListAsync();
		}
		public bool AddProductToBill(ProductBill productBill)
		{
			_context.ProductBills.Add(productBill);
			return Save();

		}
		public async Task<bool> DeleteProductFromBill(ProductBill productBill)
		{
			_context.ProductBills.Remove(productBill);
			return Save();

		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool ProductBillExists(int productId, string billNumber)
		{
			return _context.ProductBills.Any(x => x.ProductId == productId && x.BillNumber == billNumber);
		}

		public async Task<List<ProductBill>> GetProductsFromBill(string billNumber)
		{
			return await _context.ProductBills.Where(x => x.BillNumber == billNumber).ToListAsync();
		}

		public async Task<ProductBill> GetProductBill(int productId, string billNumber)
		{
			return await _context.ProductBills.FirstOrDefaultAsync(x => x.ProductId == productId && x.BillNumber == billNumber);
		}
	}
}
