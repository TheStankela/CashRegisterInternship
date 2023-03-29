using CashRegister.Domain.Models;

namespace CashRegister.Infrastructure.Repositories
{
	public interface IProductBillRepository
	{
		bool AddProductToBill(ProductBill productBill);
		Task<List<ProductBill>> GetAllProductBills();
		bool Save();
		Task<bool> DeleteProductFromBill(ProductBill productBill);
		bool ProductBillExists(int productId, string billNumber);
		Task<List<ProductBill>> GetProductsFromBill(string billNumber);
		Task<ProductBill> GetProductBill(int productId, string billNumber);
	}
}