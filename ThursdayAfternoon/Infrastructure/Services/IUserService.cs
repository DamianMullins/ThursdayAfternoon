using System;
using System.Linq;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Services
{
    public interface IUserService
    {
        IQueryable<User> Table { get; }

        User GetById(int userId);
        void Insert(User user);
        void Update(User user);
        void Delete(User user);

        Guid? AuthenticateUser(string username, string password);
    }
}
