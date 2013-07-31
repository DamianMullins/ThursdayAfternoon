using System;

namespace ThursdayAfternoon.Models
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}