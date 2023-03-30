using CashRegister.Domain.Models;

namespace CashRegister.Application.Interfaces
{
    public interface IPriceCalculatorService
    {
        Task<int> GetTotalPrice(string billNumber);
        public decimal CurrencyExchange(decimal totalBillPrice, string currency);
	}
}