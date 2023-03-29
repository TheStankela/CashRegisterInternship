using CashRegister.API.Dto;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Services
{
	public interface IBillService
	{
		Task<bool> AddBill(BillDto billDto);
		Task<bool> DeleteBill(string billNumber);
		Task<List<Bill>> GetAllBillsAsync();
		Task<Bill> GetBillByBillNumberAsync(string billNumber);
		bool UpdateBill(string billNumber, BillDto billDto);
	}
}