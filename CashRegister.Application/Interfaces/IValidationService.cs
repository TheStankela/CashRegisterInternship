using CashRegister.API.Dto;

namespace CashRegister.Application.Interfaces
{
    public interface IValidationService
    {
        bool IsValidBillNumber(string billNumber);
        bool isValidCreditCard(string creditCard);
        bool ValidateBill(AddBillDto billDto);

	}
}