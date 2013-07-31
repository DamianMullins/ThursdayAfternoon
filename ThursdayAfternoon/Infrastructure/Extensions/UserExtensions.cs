using Omu.ValueInjecter;
using ThursdayAfternoon.Models;
using ThursdayAfternoon.ViewModels.Index;

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
    }
}
