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

        //public static T BindToModel<T>(this Presentation presentation) where T : new()
        //{
        //    var viewModel = new T();
        //    return (T)viewModel.InjectFrom(presentation);
        //}
    }
}
