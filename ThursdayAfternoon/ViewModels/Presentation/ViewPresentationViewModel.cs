using System;
using System.Collections.Generic;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.ViewModels.Presentation
{
    public class ViewPresentationViewModel
    {
        public Models.Presentation Presentation { get; set; }
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int OwnerId { get; set; }
        //public User Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Slide> Slides { get; set; }
    }
}
