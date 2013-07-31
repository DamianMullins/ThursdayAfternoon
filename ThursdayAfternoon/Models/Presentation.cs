using System.Collections.Generic;

namespace ThursdayAfternoon.Models
{
    public class Presentation : BaseEntity
    {
        public string Title { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Slide> Slides { get; set; }
    }
}