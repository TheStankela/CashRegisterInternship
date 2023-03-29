using CashRegister.Application.Interfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister.Application.Services
{
    public class PriceCalculatorService : IPriceCalculatorService
	{
		private readonly IBillRepository _billRepository;
		private readonly IProductRepository _productRepository;
		private readonly IProductBillRepository _productBillRepository;

		public PriceCalculatorService(IBillRepository billRepository, IProductRepository productRepository, IProductBillRepository productBillRepository)
		{
			_billRepository = billRepository;
			_productRepository = productRepository;
			_productBillRepository = productBillRepository;
		}
		public async Task<int> GetTotalPrice(string billNumber)
		{
			var billItems = await _productBillRepository.GetProductsFromBill(billNumber);

			int totalPrice = 0;
			foreach (var item in billItems)
			{
				totalPrice += item.ProductsPrice;
			}

			return totalPrice;
		}


	}
}
