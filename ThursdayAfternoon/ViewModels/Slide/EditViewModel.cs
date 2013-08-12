using System;

namespace ThursdayAfternoon.ViewModels.Slide
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public int PresentationId { get; set; }
    }
}
