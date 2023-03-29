using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Application.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Repositories;

namespace CashRegister.Application.Services
{
	public class ProductBillService : IProductBillService
	{
		private readonly IProductBillRepository _productBillRepository;
		private readonly IMapper _mapper;
		private readonly IProductService _productService;
		private readonly IBillService _billService;
		private readonly IPriceCalculatorService _priceCalculatorService;

		public ProductBillService(IProductBillRepository productBillRepository, IPriceCalculatorService priceCalculatorService, IMapper mapper, IProductService productService, IBillService billService)
		{
			_productBillRepository = productBillRepository;
			_mapper = mapper;
			_productService = productService;
			_priceCalculatorService = priceCalculatorService;
			_billService = billService;
		}
		public async Task<bool> AddProductToBill(int productId, string billNumber, ProductBillDto productBillDto)
		{
			if (_productBillRepository.ProductBillExists(productId, billNumber))
				return false;

			var bill = await _billService.GetBillByBillNumberAsync(billNumber);
			var product = await _productService.GetProductByIdAsync(productId);
			var currentTotalPrice = await _priceCalculatorService.GetTotalPrice(billNumber);


			var productBillMapped = _mapper.Map<ProductBill>(productBillDto);
			productBillMapped.Product = product;
			productBillMapped.Bill = bill;
			productBillMapped.ProductsPrice = product.Price * productBillMapped.ProductQuantity;
			productBillMapped.Bill.TotalPrice = currentTotalPrice + productBillMapped.ProductsPrice;

			return _productBillRepository.AddProductToBill(productBillMapped);
		}

		public async Task<bool> DeleteProductFromBill(int productId, string billNumber)
		{
			if (!_productBillRepository.ProductBillExists(productId, billNumber))
				return false;

			var productBill = await _productBillRepository.GetProductBill(productId, billNumber);
			
			var bill = await _billService.GetBillByBillNumberAsync(billNumber);
			
			bill.TotalPrice -= productBill.ProductsPrice;

			return await _productBillRepository.DeleteProductFromBill(productBill);
		}

		public Task<List<ProductBill>> GetAllProductBills()
		{
			return _productBillRepository.GetAllProductBills();
		}
	}
}
