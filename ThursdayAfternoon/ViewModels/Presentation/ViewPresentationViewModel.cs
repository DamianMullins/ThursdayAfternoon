using System;
using System.Collections.Generic;

namespace ThursdayAfternoon.ViewModels.Presentation
{
    public class ViewPresentationViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int OwnerId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public Models.Presentation Presentation { get; set; }
        public List<Models.Slide> Slides { get; set; }
    }
}
