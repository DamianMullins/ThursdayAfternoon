using System;

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
        public int[] SlideIds { get; set; }
    }
}
