using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;

namespace GameStore.Application.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IUnitOfWork unitOfWork, IPublisherRepository publisherRepository)
        {
            _unitOfWork = unitOfWork;
            _publisherRepository = publisherRepository;
        }

        public Publisher GetByName(string publisherName)
        {
            Publisher publisher = _publisherRepository.GetByName(publisherName);

            return publisher;
        }

        public bool IsExist(string publisherName)
        {
            return _publisherRepository.IsExist(publisherName);
        }

        public void Create(Publisher publisher)
        {
            _publisherRepository.Insert(publisher);

            _unitOfWork.Commit();
        }

        public ICollection<Publisher> GetAll()
        {
            var publishers = _publisherRepository.GetAll();

            return publishers;
        }

        public ICollection<string> GetNames()
        {
            return _publisherRepository.GetAll().Select(x => x.CompanyName).ToList();
        }

        public void Edit(Publisher publisher)
        {
            _publisherRepository.Update(publisher);

            _unitOfWork.Commit();
        }

        public void Delete(string publisherName)
        {
            _publisherRepository.Delete(publisherName);

            _unitOfWork.Commit();
        }
    }
}
