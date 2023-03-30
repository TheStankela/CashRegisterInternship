using CashRegister.API.Dto;
using CashRegister.Application.Dto;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Services
{
	public interface IBillService
	{
		bool AddBill(AddBillDto billDto);
		bool DeleteBill(Bill bill);
		Task<List<DisplayBillDto>> GetAllBillsAsync();
		Task<DisplayBillDto> GetBillByBillNumberAsync(string billNumber);
		bool UpdateBill(string billNumber, AddBillDto billDto);
		Task<DisplayBillDto> DisplayBill(DisplayBillDto billDto, string currency);
		bool BillExists(string billNumber);
		public Task<Bill> GetBillByBillNumberAsNoTracking(string billNumber);
	}
}