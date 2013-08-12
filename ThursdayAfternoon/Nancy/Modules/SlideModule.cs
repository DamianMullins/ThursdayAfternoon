using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Validation;
using ThursdayAfternoon.Infrastructure.Extensions;
using ThursdayAfternoon.Infrastructure.Services;
using ThursdayAfternoon.Models;
using ThursdayAfternoon.ViewModels.Slide;

namespace ThursdayAfternoon.Nancy.Modules
{
    public class SlideModule : NancyModule
    {
        private readonly ISlideService _slideService;

        public SlideModule(ISlideService slideService)
            : base("/slide")
        {
            // Authentication
            this.RequiresAuthentication();
            this.RequiresClaims(new[] { "Admin" });

            // Dependency Injection
            _slideService = slideService;

            // Routes
            Get["/create/{id}"] = _ =>
            {
                //this.RequiresAuthentication();
                var model = new EditViewModel();
                return View["create", model];
            };
            Post["/create/{presentationId}"] = _ =>
            {
                EditViewModel model = this.Bind();
                ModelValidationResult result = this.Validate(model);
                if (result.IsValid)
                {
                    Slide slide = model.Bind();
                    _slideService.Insert(slide);

                    return Response.AsRedirect("/presentation");
                }
                return View["create", model];
            };

            Get["/edit/{id}/{sid}"] = _ =>
            {
                return View["edit", (int)_.sid];
            };
        }
    }
}
