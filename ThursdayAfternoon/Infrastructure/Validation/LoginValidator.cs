using FluentValidation;
using ThursdayAfternoon.ViewModels.Index;

namespace ThursdayAfternoon.Infrastructure.Validation
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Please enter a username");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Please enter a password");
        }
    }
}
