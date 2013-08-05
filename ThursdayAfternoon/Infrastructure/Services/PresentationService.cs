using System;
using System.Collections.Generic;
using System.Linq;
using ThursdayAfternoon.Infrastructure.Data;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Services
{
    public class PresentationService : IPresentationService
    {
        private readonly IRepository<Presentation> _presentationRepository;
        private readonly IRepository<Slide> _slideRepository;

        public IQueryable<Presentation> Table
        {
            get { return _presentationRepository.Table; }
        }

        public PresentationService(IRepository<Presentation> presentationRepository, IRepository<Slide> slideRepository)
        {
            _presentationRepository = presentationRepository;
            _slideRepository = slideRepository;
        }

        public Presentation GetById(int presentationId)
        {
            return _presentationRepository.GetById(presentationId);
        }

        public void Insert(Presentation presentation)
        {
            presentation.CreatedOn = DateTime.Now;
            presentation.ModifiedOn = DateTime.Now;
            _presentationRepository.Insert(presentation);
        }

        public void Update(Presentation presentation)
        {
            presentation.ModifiedOn = DateTime.Now;
            _presentationRepository.Update(presentation);
        }

        public void Delete(Presentation presentation)
        {
            _presentationRepository.Delete(presentation);
        }

        public List<Presentation> GetByOwnerId(int ownerId)
        {
            return _presentationRepository.Table.Where(p => p.OwnerId == ownerId).ToList();
        }

        public List<Slide> GetSlides(int presentationId)
        {
            return _slideRepository.Table.Where(s => s.PresentationId == presentationId).ToList();
        }
    }
}
