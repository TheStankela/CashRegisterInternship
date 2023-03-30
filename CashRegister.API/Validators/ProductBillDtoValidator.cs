using CashRegister.API.Dto;
using FluentValidation;

namespace CashRegister.API.Validators
{
	public class ProductBillDtoValidator : AbstractValidator<ProductBillDto>
	{
        public ProductBillDtoValidator()
        {
            RuleFor(x => x.ProductQuantity)
                .GreaterThan(0);
        }
    }
}
