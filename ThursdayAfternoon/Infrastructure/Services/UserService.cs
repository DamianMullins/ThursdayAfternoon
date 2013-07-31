using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using System;
using System.Linq;
using ThursdayAfternoon.Infrastructure.Data;
using ThursdayAfternoon.Infrastructure.Services.Security;
using ThursdayAfternoon.Models;
using Utilities.Core.Text;

namespace ThursdayAfternoon.Infrastructure.Services
{
    public class UserService : IUserService, IUserMapper
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IEncryptionService _encryptionService;

        public IQueryable<User> Table
        {
            get { return _userRepository.Table; }
        }

        public UserService(IRepository<User> userRepository, IRepository<UserRole> userRoleRepository, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _encryptionService = encryptionService;
        }

        public User GetById(int userId)
        {
            return _userRepository.GetById(userId);
        }

        public void Insert(User user)
        {
            user.Identifier = Guid.NewGuid();
            user.CreatedOn = DateTime.Now;
            user.ModifiedOn = DateTime.Now;
            _userRepository.Insert(user);
        }

        public void Update(User user)
        {
            user.ModifiedOn = DateTime.Now;
            _userRepository.Update(user);
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

        public Guid? AuthenticateUser(string username, string password)
        {
            User user = _userRepository.Table.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                string pwd = _encryptionService.CreatePasswordHash(password, user.PasswordSalt);
                if (pwd != user.PasswordHash)
                {
                    return null;
                }

                // Update last login data
                user.LastLoginDate = DateTime.Now;
                this.Update(user);

                return user.Identifier;
            }
            return null;
        }

        public bool UserNameExists(string userName)
        {
            string username = _userRepository.Table.Where(u => u.UserName == userName).Select(u => u.UserName).FirstOrDefault();
            return username.IsNotEmpty();
        }

        public void Register(User user, string password)
        {
            // Generate salt & hash values
            string saltKey = _encryptionService.CreateSaltKey(5);
            user.PasswordSalt = saltKey;
            user.PasswordHash = _encryptionService.CreatePasswordHash(password, saltKey);
            //Add to users role
            user.Roles.Add(GetUserRoleByName(UserRoleNames.Users));
            this.Insert(user);
        }

        #region IUserMapper Implementation

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            User user = _userRepository.Table.FirstOrDefault(u => u.Identifier == identifier);
            return user;
        }

        #endregion

        private UserRole GetUserRoleByName(string roleName)
        {
            return _userRoleRepository.Table.FirstOrDefault(r => r.Name == roleName);
        }
    }
}
