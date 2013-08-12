using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Validation;
using ThursdayAfternoon.Infrastructure.Extensions;
using ThursdayAfternoon.Infrastructure.Services;
using ThursdayAfternoon.Models;
using ThursdayAfternoon.Nancy.Extensions;
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
            Get["/create/{presentationId}"] = _ =>
            {
                EditViewModel model = this.Bind();
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

                    return Response.AsRedirect("/presentation/edit/" + model.PresentationId);
                }
                return View["create", model];
            };

            Get["/edit/{presentationId}/{id}"] = _ =>
            {
                User currentUser = this.CurrentUser();
                int slideId = _.id;
                Slide slide = _slideService.GetById(slideId);
                Presentation presentation = slide.Presentation;
                if (presentation.OwnerId == currentUser.Id)
                {
                    EditViewModel model = slide.BindToModel<EditViewModel, Slide>();
                    return View["edit", model];
                }
                return 404;
            };
            Post["/edit/{presentationId}/{id}"] = _ =>
            {
                EditViewModel model = this.Bind();
                ModelValidationResult result = this.Validate(model);
                if (result.IsValid)
                {
                    Slide slide = model.Bind();
                    _slideService.Update(slide);

                    return Response.AsRedirect("/presentation/edit/" + model.PresentationId);
                }
                return View["edit", model];
            };
        }
    }
}
