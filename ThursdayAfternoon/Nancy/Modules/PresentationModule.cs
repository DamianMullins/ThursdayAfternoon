﻿using System.Linq;
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
                ViewPresentationViewModel model = presentation.BindToModel<ViewPresentationViewModel>();
                return View["view", model];
            };

            Get["/create/"] = _ =>
            {
                var model = new EditViewModel();
                return View["create", model];
            };
            Post["/create/"] = _ =>
            {
                EditViewModel model = this.Bind();
                ModelValidationResult result = this.Validate(model);
                if (result.IsValid)
                {
                    Presentation presentation = model.Bind();
                    presentation.OwnerId = this.CurrentUser().Id;
                    _presentationService.Insert(presentation);

                    return Response.AsRedirect("/presentation");
                }
                return View["create", model];
            };

            Get["/edit/{id}"] = _ =>
            {
                int presId = _.id;
                Presentation presentation = _presentationService.GetById(presId);
                EditViewModel model = presentation.BindToModel<EditViewModel>();
                return View["edit", model];
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
                return View["edit", model];
            };
        }
    }
}
