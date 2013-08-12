using Nancy;
using Nancy.Extensions;
using Nancy.Security;
using Omu.ValueInjecter;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Nancy.Extensions
{
    public static class NancyExtensions
    {
        public static bool IsAuthenticated(this NancyModule module)
        {
            return module.Context.CurrentUser != null && module.Context.CurrentUser.IsAuthenticated();
        }

        public static Response RedirectIfLoggedIn(this NancyModule module)
        {
            if (module.IsAuthenticated())
            {
                const string redirectQuerystringKey = "returnUrl";
                string redirectUrl = "~/";
                var queryValue = module.Context.Request.Query[redirectQuerystringKey];

                if (queryValue.HasValue)
                {
                    string queryUrl = (string)queryValue;
                    if (module.Context.IsLocalUrl(queryUrl))
                    {
                        redirectUrl = queryUrl;
                    }
                }
                return module.Context.GetRedirect(redirectUrl);
            }
            return null;
        }

        public static User CurrentUser(this NancyModule module)
        {
            //return module.Context != null ? (User)module.Context.CurrentUser : null;
            return (User)module.Context.CurrentUser;
        }

        //public static void AddValidationErrors(this NancyModule module, ModelValidationResult result)
        //{
        //    ModelValidationError[] errors = result.Errors.ToArray();
        //    for (int i = 0; i < errors.Count(); i++)
        //    {
        //        string memberName = errors[i].MemberNames.First();
        //        string message = errors[i].GetMessage(memberName);

        //        module.AddValidationError(memberName, message);
        //    }
        //}

        public static void AddValidationError(this NancyModule module, string propertyName, string errorMessage)
        {
            module.ModelValidationResult = module.ModelValidationResult.AddError(propertyName, errorMessage);
        }

        public static T BindToModel<T, TM>(this TM input) where T : new()
        {
            var viewModel = new T();
            return (T)viewModel.InjectFrom(input);
        }
    }
}
