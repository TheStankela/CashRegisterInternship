using CashRegister.Domain.Models;

namespace CashRegister.Infrastructure.Repositories
{
	public interface IProductBillRepository
	{
		Task<bool> AddProductToBill(int productId, string billNumber, ProductBill productBill);
		Task<List<ProductBill>> GetAllProductBills();
		bool Save();
		Task<bool> DeleteProductFromBill(int productId, string billNumber);
		bool ProductBillExists(int productId, string billNumber);
	}
}