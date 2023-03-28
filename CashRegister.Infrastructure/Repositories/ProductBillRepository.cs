using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Infrastructure.Repositories
{
	public class ProductBillRepository : IProductBillRepository
	{
		private readonly CashRegisterDBContext _context;
		private readonly IProductRepository _productRepository;
		private readonly IBillRepository _billRepository;

		public ProductBillRepository(CashRegisterDBContext context, IProductRepository productRepository, IBillRepository billRepository)
		{
			_context = context;
			_productRepository = productRepository;
			_billRepository = billRepository;
		}
		public async Task<List<ProductBill>> GetAllProductBills()
		{
			return await _context.ProductBills.ToListAsync();
		}
		public async Task<bool> AddProductToBill(int productId, string billNumber, ProductBill productBill)
		{
			var productEntity = await _productRepository.GetProductByIdAsync(productId);
			var billEntity = await _billRepository.GetBillByBillNumberAsync(billNumber);

			var productBillToAdd = new ProductBill
			{
				Bill = billEntity,
				Product = productEntity,
				ProductQuantity = productBill.ProductQuantity,
				ProductsPrice = productEntity.Price * productBill.ProductQuantity
			};

			billEntity.TotalPrice += productBill.ProductsPrice;

			_context.ProductBills.Add(productBillToAdd);
			return Save();

		}
		public async Task<bool> DeleteProductFromBill(int productId, string billNumber)
		{
			var productBill = await _context.ProductBills.FirstOrDefaultAsync(x => x.ProductId == productId && x.BillNumber == billNumber);
			if (productBill == null)
				return false;

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
	}
}
