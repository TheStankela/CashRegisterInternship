using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using CashRegister.API.Dto;

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
		public async Task<bool> AddProductBill(int productId, string billNumber, ProductBillRepoDto productBillDto)
		{
			var productEntity = await _productRepository.GetProductByIdAsync(productId);
			var billEntity = await _billRepository.GetBillByBillNumberAsync(billNumber);

			var productBill = new ProductBill
			{
				Bill = billEntity,
				Product = productEntity,
				ProductQuantity = productBillDto.ProductQuantity,
				ProductsPrice = productEntity.Price * productBillDto.ProductQuantity
			};

			billEntity.TotalPrice += productBill.ProductsPrice;
			


			_context.ProductBills.Add(productBill);
			return Save();

		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

	}
}
