using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;

namespace GameStore.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenreRepository _genreRepository;

        public GenreService(IUnitOfWork unitOfWork, IGenreRepository genreRepository)
        {
            _unitOfWork = unitOfWork;
            _genreRepository = genreRepository;
        }

        public void Create(Genre genre)
        {
            _genreRepository.Insert(genre);

            _unitOfWork.Commit();
        }

        public ICollection<Genre> GetAll()
        {
            return _genreRepository.GetAll();
        }

        public ICollection<string> GetNames()
        {
            return _genreRepository.GetAll().Select(x => x.Name).ToList();
        }

        public void Edit(Genre genre)
        {
            _genreRepository.Update(genre);

            _unitOfWork.Commit();
        }

        public void Delete(string genreName)
        {
            _genreRepository.Delete(genreName);

            _unitOfWork.Commit();
        }
    }
}
