namespace CashRegister.API.Dto
{
	public class BillDto
	{
		public string BillNumber { get; set; }
		public string PaymentMethod { get; set; }
		public int TotalPrice { get; set; }
		public string CreditCardNumber { get; set; }
	}
}
