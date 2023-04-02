using CashRegister.API.Dto;
using FluentValidation;

namespace CashRegister.API.Validators
{
	public class ProductDtoValidator : AbstractValidator<ProductDto>
	{
        public ProductDtoValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Name)
                .NotNull()
                .Matches("^[a-zA-Z0-9 ]*$")
                .WithMessage("Name is not valid.");
        }
    }
}
