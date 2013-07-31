using System.Collections.Generic;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.ViewModels.Presentation
{
    public class IndexViewModel
    {
        public User User { get; set; }
        public List<Models.Presentation> Presentations { get; set; }
    }
}
