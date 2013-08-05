using Omu.ValueInjecter;
using ThursdayAfternoon.Models;
using ThursdayAfternoon.ViewModels.Index;
using Utilities.Core.Text;

namespace ThursdayAfternoon.Infrastructure.Extensions
{
    public static class UserExtensions
    {
        public static User Bind(this RegisterViewModel viewModel)
        {
            var user = new User();
            return (User)user.InjectFrom(viewModel);
        }

        public static T BindToModel<T>(this User presentation) where T : new()
        {
            var viewModel = new T();
            return (T)viewModel.InjectFrom(presentation);
        }

        public static string GetName(this User user, bool fullname = false)
        {
            if (user.FirstName.IsNotEmpty())
            {
                if (user.LastName.IsNotEmpty() && fullname)
                {
                    return "{0} {1}".With(user.FirstName, user.LastName);
                }
                return user.FirstName;
            }
            return user.UserName;
        }
    }
}
