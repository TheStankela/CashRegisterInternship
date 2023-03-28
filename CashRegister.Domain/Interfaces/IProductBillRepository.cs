using CashRegister.API.Dto;
using CashRegister.Domain.Models;

namespace CashRegister.Infrastructure.Repositories
{
	public interface IProductBillRepository
	{
		Task<bool> AddProductBill(int productId, string billNumber, ProductBillRepoDto productBillDto);
		Task<List<ProductBill>> GetAllProductBills();
	}
}