using FluentValidation;
using ThursdayAfternoon.ViewModels.Index;

namespace ThursdayAfternoon.Infrastructure.Validation
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage("Please enter a username");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Please enter a password");
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("Please confirm password");
            RuleFor(p => p.Password).Equal(p => p.ConfirmPassword).WithMessage("Passwords do not match");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Please enter an email address");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Email address is not valid");
        }
    }
}
