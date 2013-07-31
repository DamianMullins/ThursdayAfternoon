using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.ViewModels.Presentation
{
    public class EditViewModel
    {
        //public Models.Presentation Presentation { get; set; }
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public List<Slide> Slides { get; set; }
    }
}
