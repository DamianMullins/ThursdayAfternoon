using Omu.ValueInjecter;
using ThursdayAfternoon.Models;
using ThursdayAfternoon.ViewModels.Presentation;
using Utilities.Web.Url;

namespace ThursdayAfternoon.Infrastructure.Extensions
{
    public static class PresentationExtensions
    {
        public static Presentation Bind(this EditViewModel viewModel)
        {
            var presentation = new Presentation();
            return (Presentation)presentation.InjectFrom(viewModel);
        }

        public static T BindToModel<T>(this Presentation presentation) where T : new()
        {
            var viewModel = new T();
            return (T)viewModel.InjectFrom(presentation);
        }

        public static string Slug(this Presentation presentation)
        {
            return presentation.Title.ToSlug();
        }
    }
}
