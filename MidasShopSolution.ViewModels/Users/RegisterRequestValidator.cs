using FluentValidation;

namespace MidasShopSolution.ViewModels.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("First name is required")
            .MaximumLength(200).WithMessage("First name cannot over 200 characters");

            RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is required")
            .MaximumLength(200).WithMessage("Last name cannot over 200 characters");

            RuleFor(r => r.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday cannot greater than 100 years");

            RuleFor(r => r.Email).NotEmpty().WithMessage("Email is required")
            .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email not match");

            RuleFor(r => r.PhoneNumber).NotEmpty().WithMessage("Phone number is required")
            .MaximumLength(200).WithMessage("Phone number cannot over 200 characters");

            RuleFor(r => r.UserName).NotEmpty().WithMessage("Username is required");

            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password at least 6 characters");

            RuleFor(r => r).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password is not match");
                }
            });
        }
    }
}