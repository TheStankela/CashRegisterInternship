
namespace CashRegister.Application.Dto
{
	public class DisplayBillDto
	{
		public string BillNumber { get; set; }
		public decimal TotalPrice { get; set;}
		public string PaymentMethod { get; set; }
		public string CreditCardNumber { get; set; }
	}
}
