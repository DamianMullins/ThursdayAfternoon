using System;
using System.Collections.Generic;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.ViewModels.Presentation
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Slide> Slides { get; set; }

        public EditViewModel()
        {
            Slides = new List<Slide>();
        }
    }
}
