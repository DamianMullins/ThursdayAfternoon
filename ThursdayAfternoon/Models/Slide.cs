using System;

namespace ThursdayAfternoon.Models
{
    public class Slide : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Presentation Presentation { get; set; }
        public int PresentationId { get; set; }
    }
}