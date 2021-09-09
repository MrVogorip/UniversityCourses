using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;

namespace GameStore.Application.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlatformRepository _platformRepository;

        public PlatformService(IUnitOfWork unitOfWork, IPlatformRepository platformRepository)
        {
            _unitOfWork = unitOfWork;
            _platformRepository = platformRepository;
        }

        public void Create(Platform platform)
        {
            _platformRepository.Insert(platform);

            _unitOfWork.Commit();
        }

        public ICollection<Platform> GetAll()
        {
            return _platformRepository.GetAll();
        }

        public ICollection<string> GetNames()
        {
            return _platformRepository.GetAll().Select(x => x.Name).ToList();
        }

        public void Edit(Platform platform)
        {
            _platformRepository.Update(platform);

            _unitOfWork.Commit();
        }

        public void Delete(string platformName)
        {
            _platformRepository.Delete(platformName);

            _unitOfWork.Commit();
        }
    }
}
