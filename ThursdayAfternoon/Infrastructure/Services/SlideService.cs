using System;
using System.Collections.Generic;
using System.Linq;
using ThursdayAfternoon.Infrastructure.Data;
using ThursdayAfternoon.Models;

namespace ThursdayAfternoon.Infrastructure.Services
{
    public class SlideService : ISlideService
    {
        private readonly IRepository<Slide> _slideRepository;
        
        public IQueryable<Slide> Table
        {
            get { return _slideRepository.Table; }
        }

        public SlideService(IRepository<Slide> slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public Slide GetById(int slideId)
        {
            return _slideRepository.GetById(slideId);
        }

        public void Insert(Slide slide)
        {
            slide.CreatedOn = DateTime.Now;
            slide.ModifiedOn = DateTime.Now;
            _slideRepository.Insert(slide);
        }

        public void Update(Slide slide)
        {
            slide.ModifiedOn = DateTime.Now;
            _slideRepository.Update(slide);
        }

        public void Delete(Slide slide)
        {
            _slideRepository.Delete(slide);
        }

        public List<Slide> GetByPresentationId(int presentationId)
        {
            return _slideRepository.Table.Where(s => s.PresentationId == presentationId).ToList();
        }
    }
}