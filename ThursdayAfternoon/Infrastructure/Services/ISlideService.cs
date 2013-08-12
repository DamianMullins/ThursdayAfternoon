using System.Collections.Generic;
using System.Linq;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Services
{
    public interface ISlideService
    {
        IQueryable<Slide> Table { get; }

        Slide GetById(int slideId);
        void Insert(Slide slide);
        void Update(Slide slide);
        void Delete(Slide slide);

        List<Slide> GetByPresentationId(int presentationId);
    }
}
