using CashRegister.API.Dto;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Interfaces
{
	public interface IProductBillService
	{
		Task<bool> AddProductToBill(int productId, string billNumber, ProductBillDto productBillDto);
		Task<List<ProductBill>> GetAllProductBills();
		Task<bool> DeleteProductFromBill(int productId, string billNumber);
	}
}
