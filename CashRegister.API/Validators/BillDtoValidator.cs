using CashRegister.API.Dto;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CashRegister.API.Validators
{
	public class BillDtoValidator : AbstractValidator<AddBillDto>
	{
		public BillDtoValidator() 
		{
			RuleFor(x => x.PaymentMethod)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.CreditCardNumber)
				.CreditCard()
				.NotEmpty()
				.WithMessage("You must enter a valid credit card number");
			RuleFor(x => x.BillNumber)
				.NotNull()
				.NotEmpty()
				.Must(ValidBillNumber)
				.WithErrorCode("400")
				.WithMessage("You must enter a valid bill number");

		}
		public bool ValidBillNumber(string billNumber)
		{
			Regex regex = new Regex(@"^\d{3}-\d{13}-\d{2}$");
			if (!regex.IsMatch(billNumber))
			{
				return false;
			}

			string identificationCode = billNumber.Substring(0, 3);
			string billNum = billNumber.Substring(4, 13);
			string controlNum = billNumber.Substring(18, 2);
			string concatenatedNum = identificationCode + billNum;
			long multipliedNum = long.Parse(concatenatedNum) * 100;
			int controlCalc = 98 - (int)(multipliedNum % 97);
			int controlNumInt = int.Parse(controlNum);
			return controlCalc == controlNumInt;
		}
	}
}
