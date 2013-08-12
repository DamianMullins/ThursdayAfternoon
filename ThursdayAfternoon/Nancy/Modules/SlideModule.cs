using Nancy;
using ThursdayAfternoon.Infrastructure.Services;

namespace ThursdayAfternoon.Nancy.Modules
{
    public class SlideModule : NancyModule
    {
        private readonly ISlideService _slideService;

        public SlideModule(ISlideService slideService)
            : base("/slide")
        {
            // Dependency Injection
            _slideService = slideService;

            Get["/create/{id}"] = _ =>
            {
                return View["create", (int)_.id];
            };

            Get["/edit/{id}/{sid}"] = _ =>
            {
                return View["edit", (int)_.sid];
            };
        }
    }
}
