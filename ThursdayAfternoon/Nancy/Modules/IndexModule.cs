using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using Nancy.Validation;
using System;
using ThursdayAfternoon.Infrastructure.Extensions;
using ThursdayAfternoon.Infrastructure.Services;
using ThursdayAfternoon.Models;
using ThursdayAfternoon.Nancy.Extensions;
using ThursdayAfternoon.ViewModels.Index;

namespace ThursdayAfternoon.Nancy.Modules
{
    public class IndexModule : NancyModule
    {
        private readonly IUserService _userService;
        public IndexModule(IUserService userService)
        {
            // Dependency Injection
            _userService = userService;

            // Pipelines
            Before += context => context.Request.Path == "/login" ? this.RedirectIfLoggedIn() : null;

            // Routes
            Get["/"] = _ => View["index"];

            Get["/login"] = _ => View["login", new LoginViewModel()];
            Post["/login"] = _ =>
            {
                LoginViewModel model = this.Bind();
                ModelValidationResult result = this.Validate(model);
                if (result.IsValid)
                {
                    Guid? userIdentifier = _userService.AuthenticateUser(model.UserName, model.Password);
                    if (userIdentifier == null)
                    {
                        this.AddValidationError("", "The login details you provided were not correct");
                        return View["login", model];
                    }

                    DateTime? expiry = null;
                    if (model.RememberMe.HasValue)
                    {
                        expiry = DateTime.Now.AddDays(7);
                    }
                    return this.Login(userIdentifier.Value, expiry);
                }
                return View["login", model];
            };

            Get["/logout"] = _ =>
            {
                return this.LogoutAndRedirect("~/");
            };

            Get["/register"] = _ => View["register", new RegisterViewModel()];
            Post["/register"] = _ =>
            {
                RegisterViewModel model = this.Bind();
                
                // Check username is unique
                bool usernameExists = _userService.UserNameExists(model.UserName);
                if (usernameExists)
                {
                    this.AddValidationError("UserName", "Username already exists");
                    return View["register", model];
                }

                ModelValidationResult result = this.Validate(model);
                if (result.IsValid)
                {
                    User user = model.Bind();
                    _userService.Register(user, model.Password);

                    return Response.AsRedirect("/presentation");
                }
                return View["register", model];
            };
        }
    }
};