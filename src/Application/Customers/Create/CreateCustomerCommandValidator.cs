using FluentValidation;

namespace Application.Customers.Create
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(r => r.LastName)
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Last name");

            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);

            RuleFor(r => r.PhoneNumber)
                .NotEmpty()
                .Length(9)
                .WithName("Phone number");
        }
    }
}
