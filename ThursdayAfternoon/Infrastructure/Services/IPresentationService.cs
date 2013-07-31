using System.Collections.Generic;
using System.Linq;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Services
{
    public interface IPresentationService
    {
        IQueryable<Presentation> Table { get; }

        Presentation GetById(int presentationId);
        void Insert(Presentation presentation);
        void Update(Presentation presentation);
        void Delete(Presentation presentation);

        List<Presentation> GetByOwnerId(int ownerId);
    }
}
