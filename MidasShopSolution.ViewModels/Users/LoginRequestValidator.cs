using FluentValidation;

namespace MidasShopSolution.ViewModels.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Username is required");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password at least 6 characters");
        }
    }
}