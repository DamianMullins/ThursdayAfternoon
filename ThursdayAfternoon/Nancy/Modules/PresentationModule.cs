using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Nancy.Validation;
using Omu.ValueInjecter;
using System.Collections.Generic;
using ThursdayAfternoon.Infrastructure.Extensions;
using ThursdayAfternoon.Infrastructure.Services;
using ThursdayAfternoon.Models;
using ThursdayAfternoon.Nancy.Extensions;
using ThursdayAfternoon.ViewModels.Presentation;

namespace ThursdayAfternoon.Nancy.Modules
{
    public class PresentationModule : NancyModule
    {
        private readonly IPresentationService _presentationService;

        public PresentationModule(IPresentationService presentationService)
            : base("/presentation")
        {
            // Authentication
            this.RequiresAuthentication();
            this.RequiresClaims(new[] { "Admin" });

            // Dependency Injection
            _presentationService = presentationService;

            // Routes
            Get["/"] = _ =>
            {
                User currentUser = this.CurrentUser();
                List<Presentation> presentations = _presentationService.GetByOwnerId(currentUser.Id);
                var model = new IndexViewModel { User = currentUser, Presentations = presentations };
                return View["index", model];
            };

            Get["/view/{id}"] = _ =>
            {
                int presId = _.id;
                Presentation presentation = _presentationService.GetById(presId);
                ViewPresentationViewModel model = presentation.BindToModel<ViewPresentationViewModel, Presentation>();
                return View["view", model];
            };

            Get["/create/"] = _ =>
            {
                var model = new EditViewModel();
                return View["create", model];
            };
            Post["/create/"] = _ =>
            {
                User currentUser = this.CurrentUser();
                EditViewModel model = this.Bind();
                ModelValidationResult result = this.Validate(model);
                if (result.IsValid)
                {
                    Presentation presentation = model.Bind();
                    presentation.OwnerId = currentUser.Id;
                    _presentationService.Insert(presentation);

                    return Response.AsRedirect("/presentation");
                }
                return View["create", model];
            };

            Get["/edit/{id}"] = _ =>
            {
                User currentUser = this.CurrentUser();
                int presId = _.id;
                Presentation presentation = _presentationService.GetById(presId);
                if (presentation.OwnerId == currentUser.Id)
                {
                    EditViewModel model = presentation.BindToModel<EditViewModel, Presentation>();
                    model.SlideIds = presentation.Slides.Select(s => s.Id).ToArray();
                    return View["edit", model];
                }
                return 404;
            };
            Post["/edit/{id}"] = _ =>
            {
                EditViewModel model = this.Bind();
                ModelValidationResult result = this.Validate(model);
                if (result.IsValid)
                {
                    Presentation presentation = model.Bind();
                    _presentationService.Update(presentation);

                    return Response.AsRedirect("/presentation");
                }
                //model.SlideIds = this.GetSlides(_.id);
                return View["edit", model];
            };
        }
    }
}
