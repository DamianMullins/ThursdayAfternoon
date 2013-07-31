using System;
using System.Linq;
using ThursdayAfternoon.Infrastructure.Data;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IRepository<UserRole> _userRoleRepository;

        public IQueryable<UserRole> Table
        {
            get { return _userRoleRepository.Table; }
        }

        public UserRoleService(IRepository<UserRole> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public UserRole GetById(int userRoleId)
        {
            return _userRoleRepository.GetById(userRoleId);
        }

        public void Insert(UserRole userRole)
        {
            userRole.CreatedOn = DateTime.Now;
            userRole.ModifiedOn = DateTime.Now;
            _userRoleRepository.Insert(userRole);
        }

        public void Update(UserRole userRole)
        {
            userRole.ModifiedOn = DateTime.Now;
            _userRoleRepository.Update(userRole);
        }

        public void Delete(UserRole userRole)
        {
            _userRoleRepository.Delete(userRole);
        }
    }
}
