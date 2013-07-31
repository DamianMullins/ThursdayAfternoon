using System;
using System.Linq;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Services
{
    public interface IUserRoleService
    {
        IQueryable<UserRole> Table { get; }

        UserRole GetById(int userRoleId);
        void Insert(UserRole userRole);
        void Update(UserRole userRole);
        void Delete(UserRole userRole);
    }
}
