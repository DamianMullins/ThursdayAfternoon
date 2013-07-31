using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;
using Nancy.Validation;
using System;
using ThursdayAfternoon.Infrastructure.Services;
using ThursdayAfternoon.Infrastructure.Services.Security;
using ThursdayAfternoon.Models;
using ThursdayAfternoon.Nancy.Extensions;
using ThursdayAfternoon.ViewModels.Index;

namespace ThursdayAfternoon.Nancy.Modules
{
    public class IndexModule : NancyModule
    {
        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptionService;

        //private readonly User _currentUser;

        public IndexModule(IUserService userService, IEncryptionService encryptionService)
        {
            // Dependency Injection
            _userService = userService;
            _encryptionService = encryptionService;

            // Private Properties
            //_currentUser = this.CurrentUser();

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

            Get["/register"] = _ => "TODO";
            Post["/register"] = _ =>
            {
                var model = this.Bind<RegisterViewModel>();
                // TODO: Move this out to user service
                var user = new User();
                string saltKey = _encryptionService.CreateSaltKey(5);

                // Generate salt & hash values
                user.PasswordSalt = saltKey;
                user.PasswordHash = _encryptionService.CreatePasswordHash(model.Password, saltKey);

                return "TODO";
            };
        }
    }
};