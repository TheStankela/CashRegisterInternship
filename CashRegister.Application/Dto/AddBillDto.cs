namespace CashRegister.API.Dto
{
	public class AddBillDto
	{
		public string BillNumber { get; set; }
		public string PaymentMethod { get; set; }
		public string CreditCardNumber { get; set; }
	}
}
