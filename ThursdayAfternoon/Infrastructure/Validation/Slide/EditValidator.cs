using FluentValidation;
using ThursdayAfternoon.ViewModels.Slide;

namespace ThursdayAfternoon.Infrastructure.Validation.Slide
{
    public class EditValidator : AbstractValidator<EditViewModel>
    {
        public EditValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title must be provided.");
        }
    }
}
