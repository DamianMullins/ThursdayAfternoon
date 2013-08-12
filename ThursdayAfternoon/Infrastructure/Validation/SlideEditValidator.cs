using FluentValidation;
using ThursdayAfternoon.ViewModels.Slide;

namespace ThursdayAfternoon.Infrastructure.Validation
{
    public class SlideEditValidator : AbstractValidator<EditViewModel>
    {
        public SlideEditValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title must be provided.");
        }
    }
}
