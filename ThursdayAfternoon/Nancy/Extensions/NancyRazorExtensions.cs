using Nancy.ViewEngines.Razor;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Nancy.Extensions
{
    public static class NancyRazorExtensions
    {
        public static User User<T>(this HtmlHelpers<T> html)
        {
            return (User)html.RenderContext.Context.CurrentUser;
        }
    }
}
