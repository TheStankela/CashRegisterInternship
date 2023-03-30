using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Application.Interfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Repositories;

namespace CashRegister.Application.Services
{
	public class ProductBillService : IProductBillService
	{
		private readonly IProductBillRepository _productBillRepository;
		private readonly IMapper _mapper;
		private readonly IProductRepository _productRepository;
		private readonly IBillRepository _billRepository;
		private readonly IPriceCalculatorService _priceCalculatorService;

		public ProductBillService(IProductBillRepository productBillRepository, IPriceCalculatorService priceCalculatorService, IMapper mapper, IProductRepository productRepository, IBillRepository billRepository)
		{
			_productBillRepository = productBillRepository;
			_mapper = mapper;
			_productRepository = productRepository;
			_billRepository = billRepository;
			_priceCalculatorService = priceCalculatorService;
		}
		public async Task<bool> AddProductToBill(int productId, string billNumber, ProductBillDto productBillDto)
		{
			var bill = await _billRepository.GetBillByBillNumberAsync(billNumber);
			var product = await _productRepository.GetProductByIdAsync(productId);

			var currentTotalPrice = await _priceCalculatorService.GetTotalPrice(billNumber);

			var productBillMapped = _mapper.Map<ProductBill>(productBillDto);
			productBillMapped.Product = product;
			productBillMapped.Bill = bill;
			productBillMapped.ProductsPrice = product.Price * productBillMapped.ProductQuantity;
			productBillMapped.Bill.TotalPrice = currentTotalPrice + productBillMapped.ProductsPrice;

			if (productBillMapped.Bill.TotalPrice > 50000)
				return false;

			return _productBillRepository.AddProductToBill(productBillMapped);
		}

		public async Task<bool> DeleteProductFromBill(ProductBill productBill)
		{
			var bill = await _billRepository.GetBillByBillNumberAsync(productBill.BillNumber);
			
			bill.TotalPrice -= productBill.ProductsPrice;

			return await _productBillRepository.DeleteProductFromBill(productBill);
		}

		public Task<List<ProductBill>> GetAllProductBills()
		{
			return _productBillRepository.GetAllProductBills();
		}
		public bool ProductBillExists(int productId, string billNumber)
		{
			return _productBillRepository.ProductBillExists(productId, billNumber);
		}
		public async Task<ProductBill> GetProductBill(int productId, string billNumber)
		{
			return await _productBillRepository.GetProductBill(productId, billNumber);
		}
	}
}
