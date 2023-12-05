using FluentValidation;

namespace Application.Features.Customer.Commands.Insert
{
    public class InsertCommandValidation : AbstractValidator<InsertCommand>
    {
        public InsertCommandValidation()
        {
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("{Email} is required")
                .NotNull()
                .EmailAddress().WithMessage("{Email} must not exceed 50 characters");

            RuleFor(a => a.Firstname)
              .NotEmpty().WithMessage("{Firstname} is required");

            RuleFor(a => a.Lastname)
              .NotEmpty().WithMessage("{Lastname} is required");

            RuleFor(a => a.PhoneNumber)
              .NotEmpty().WithMessage("{PhoneNumber} is required");

            RuleFor(a => a.BankAccountNumber)
                .NotEmpty().WithMessage("{BankAccountNumber} is required");
        }

       
    }
}
