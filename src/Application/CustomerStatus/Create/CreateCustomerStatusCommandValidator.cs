using FluentValidation;

namespace Application.Customers.Status.Create
{
    public class CreateCustomerStatusCommandValidator : AbstractValidator<CreateCustomerStatusCommand>
    {
        public CreateCustomerStatusCommandValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
