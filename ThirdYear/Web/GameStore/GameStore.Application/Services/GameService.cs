using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Enums;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;

namespace GameStore.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameRepository _gameRepository;
        private readonly IPublisherRepository _publisherRepository;

        public GameService(IUnitOfWork unitOfWork, IGameRepository gameRepository, IPublisherRepository publisherRepository)
        {
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
            _publisherRepository = publisherRepository;
        }

        public bool IsExist(string gameKey)
        {
            return _gameRepository.IsExist(gameKey);
        }

        public void Create(Game game, ICollection<string> platformNames, ICollection<string> genreNames, string publisherName)
        {
            var publisher = _publisherRepository.GetByName(publisherName);
            game.PublisherId = publisher.Id;

            _gameRepository.Create(game, platformNames, genreNames);

            _unitOfWork.Commit();
        }

        public void Edit(Game game)
        {
            _gameRepository.Update(game);

            _unitOfWork.Commit();
        }

        public void Delete(string gameKey)
        {
            _gameRepository.Delete(gameKey);

            _unitOfWork.Commit();
        }

        public Game GetByKey(string gameKey)
        {
            var game = _gameRepository.GetByKey(gameKey);

            return game;
        }

        public Game GetByKeyForOrder(string gameKey)
        {
            var game = _gameRepository.GetByKeyForOrder(gameKey);

            return game;
        }

        public ICollection<Game> GetAll()
        {
            return _gameRepository.GetAll(game => !game.IsDeleted);
        }

        public ICollection<Game> GetAll(Func<Game, bool> predicate)
        {
            return _gameRepository.GetAll(predicate);
        }

        public ICollection<Game> GetAll(Expression<Func<Game, bool>> expression, SortingCriterion sorting, PaginItemQuantity paginItem, int page)
        {
            ICollection<Game> games;

            switch (sorting)
            {
                case SortingCriterion.MostPopular:
                    games = _gameRepository.GetAll(expression, x => x.AmountViews, false, paginItem, page);
                    break;
                case SortingCriterion.MostCommented:
                    games = _gameRepository.GetAll(expression, x => x.Comments.Count(), false, paginItem, page);
                    break;
                case SortingCriterion.ByPriceAsc:
                    games = _gameRepository.GetAll(expression, x => x.Price, true, paginItem, page);
                    break;
                case SortingCriterion.ByPriceDesc:
                    games = _gameRepository.GetAll(expression, x => x.Price, false, paginItem, page);
                    break;
                case SortingCriterion.NewByDate:
                    games = _gameRepository.GetAll(expression, x => x.UploadDate, false, paginItem, page);
                    break;
                default:
                    games = _gameRepository.GetAll(expression, x => x.Id, true, paginItem, page);
                    break;
            }

            return games.ToList();
        }

        public ICollection<Game> GetAllByGenre(string genreName)
        {
            var games = _gameRepository.GetAllByGenre(genreName);

            return games;
        }

        public ICollection<Game> GetAllByPlatforms(ICollection<string> platformNames)
        {
            var games = _gameRepository.GetAllByPlatforms(platformNames);

            return games;
        }

        public FileStream GetFileForDownload(string gameKey)
        {
            var fileName = gameKey + ".exe";
            var filepath = Path.Combine(@"wwwroot\\download-games", fileName);
            var fs = new FileStream(filepath, FileMode.Open);

            return fs;
        }

        public int GetCountGames()
        {
            int countGames = _gameRepository.GetCount();

            return countGames;
        }

        public int GetCountGames(Expression<Func<Game, bool>> expression)
        {
            int countGames = _gameRepository.GetCount(expression);

            return countGames;
        }
    }
}
