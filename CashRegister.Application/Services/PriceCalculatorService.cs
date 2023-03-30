using CashRegister.Application.Interfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Repositories;

namespace CashRegister.Application.Services
{
	public class PriceCalculatorService : IPriceCalculatorService
	{
		private readonly IProductBillRepository _productBillRepository;

		public PriceCalculatorService(IProductBillRepository productBillRepository)
		{
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
		public decimal CurrencyExchange(decimal totalBillPrice, string currency)
		{
			switch (currency)
			{
				case "eur":
					{
						return totalBillPrice / 117;
					}
				case "usd":
					{
						return totalBillPrice / 110;
					}
				
				default: { return totalBillPrice; }
			}
		}


	}
}
