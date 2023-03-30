using CashRegister.Domain.Models;

namespace CashRegister.Domain.Interfaces
{
	public interface IBillRepository
	{
		public bool AddBill(Bill bill);
		public Task<Bill> GetBillByBillNumberAsync(string billNumber);
		public Task<List<Bill>> GetAllBillsAsync();
		public bool Save();
		public bool DeleteBill(Bill bill);
		public bool UpdateBill(Bill bill);
		public bool BillExists(string billNumber);
		public Task<Bill> GetBillByBillNumberAsNoTracking(string billNumber);
	}
}
