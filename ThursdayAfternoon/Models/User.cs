using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ThursdayAfternoon.Models
{
    public class User : BaseEntity, IUserIdentity
    {
        private ICollection<UserRole> _userRoles;

        public string UserName { get; set; }
        public Guid Identifier { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public IEnumerable<string> Claims { get { return this.Roles.Select(r => r.Name); } }
        public virtual ICollection<UserRole> Roles
        {
            get { return _userRoles ?? (_userRoles = new List<UserRole>()); }
            protected set { _userRoles = value; }
        }

        public virtual ICollection<Presentation> Presentations { get; set; }
    }
}