using FluentValidation;
using ThursdayAfternoon.ViewModels.Presentation;

namespace ThursdayAfternoon.Infrastructure.Validation
{
    public class PresentationEditValidator : AbstractValidator<EditViewModel>
    {
        public PresentationEditValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title must be provided.");
        }
    }
}
