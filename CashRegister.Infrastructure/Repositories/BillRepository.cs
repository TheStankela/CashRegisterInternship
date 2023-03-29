using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace CashRegister.Infrastructure.Repositories
{
	public class BillRepository : IBillRepository
	{
		private readonly CashRegisterDBContext _context;
		public BillRepository(CashRegisterDBContext context)
        {
            _context = context;
        }
        public bool AddBill(Bill bill)
		{
			_context.Bills.Add(bill);
			return Save();
		}

		public bool BillExists(string billNumber)
		{
			return _context.Bills.Any(x => x.BillNumber == billNumber);
		}

		public bool DeleteBill(Bill bill)
		{
			_context.Remove(bill);
			return Save();
		}

		public Task<List<Bill>> GetAllBillsAsync()
		{
			return _context.Bills.ToListAsync();
		}

		public async Task<Bill> GetBillByBillNumberAsync(string billNumber)
		{
			return await _context.Bills.FirstOrDefaultAsync(x => x.BillNumber == billNumber);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateBill(Bill bill)
		{
			_context.Update(bill);
			return Save();
		}
	}
}
