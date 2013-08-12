using Omu.ValueInjecter;
using ThursdayAfternoon.Models;
using ThursdayAfternoon.ViewModels.Slide;

namespace ThursdayAfternoon.Infrastructure.Extensions
{
    public static class SlideExtensions
    {
        public static Slide Bind(this EditViewModel viewModel)
        {
            var slide = new Slide();
            return (Slide)slide.InjectFrom(viewModel);
        }
    }
}
