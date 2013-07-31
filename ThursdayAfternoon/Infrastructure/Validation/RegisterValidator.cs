using FluentValidation;
using ThursdayAfternoon.ViewModels.Index;

namespace ThursdayAfternoon.Infrastructure.Validation
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Please enter a username");
            RuleFor(p => p.UserName).EmailAddress().WithMessage("Email address is not valid");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Please enter a password");
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("Please confirm password");
            RuleFor(p => p.Password).Equal(p => p.ConfirmPassword).WithMessage("Passwords do not match");
        }
    }
}
