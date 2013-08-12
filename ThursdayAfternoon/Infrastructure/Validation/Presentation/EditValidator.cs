using FluentValidation;
using ThursdayAfternoon.ViewModels.Presentation;

namespace ThursdayAfternoon.Infrastructure.Validation.Presentation
{
    public class EditValidator : AbstractValidator<EditViewModel>
    {
        public EditValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title must be provided.");
        }
    }
}
