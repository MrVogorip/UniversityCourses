using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Application.Services;
using GameStore.Domain.Enums;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Application.Tests
{
    [TestClass]
    public class GameServiceTests
    {
        private GameService _gameService;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IGameRepository> _gameRepositoryMock;
        private Mock<IPublisherRepository> _publisherRepositoryMock;
        private Game _game;
        private ICollection<Game> _games;
        private string _genre;
        private ICollection<string> _genreNames;
        private ICollection<string> _platformNames;
        private Publisher _publisher;

        [TestInitialize]
        public void GameServiceTestsInitialize()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _publisherRepositoryMock = new Mock<IPublisherRepository>();
            _gameService = new GameService(_unitOfWorkMock.Object, _gameRepositoryMock.Object, _publisherRepositoryMock.Object);

            _game = new Game()
            {
                Id = "1",
                Description = "test descrp",
                Key = "2",
                Name = "test name game",
            };
            _games = new List<Game>() { _game };
            _genre = "test genres";
            _genreNames = new List<string>()
            {
                "test genres number 1",
            };
            _platformNames = new List<string>()
            {
                "test platforms number 1",
            };
            _publisher = new Publisher { Id = "123", CompanyName = "test name", Description = "test descrp", HomePage = "test home page" };
        }

        [TestMethod]
        public void CreateGame_ShouldCallRepositoryInsert_WhenGameIsValid()
        {
            string testCompanyName = "test Company Name";
            _gameRepositoryMock.Setup(x => x.Create(It.IsAny<Game>(), It.IsAny<ICollection<string>>(), It.IsAny<ICollection<string>>()));
            _publisherRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(_publisher);

            _gameService.Create(_game, _platformNames, _genreNames, testCompanyName);

            _gameRepositoryMock.Verify(x => x.Create(_game, _platformNames, _genreNames), Times.Once);
        }

        [TestMethod]
        public void GetByFunk_ShouldCallRepositoryGetByFunkAndReturnGames_Always()
        {
            _gameRepositoryMock.Setup(x => x.GetAll(It.IsAny<Func<Game, bool>>())).Returns(_games);

            ICollection<Game> games = _gameService.GetAll(x => x.IsDeleted == false);

            Assert.AreEqual(_games.Count, games.Count);
        }

        [TestMethod]
        public void GetByFunk_ShouldCallGetByFunkMethodInGameRepositoryOnce_Always()
        {
            Func<Game, bool> funcTest = x => x.IsDeleted == false;
            _gameRepositoryMock.Setup(x => x.GetAll(It.IsAny<Func<Game, bool>>())).Returns(_games);

            ICollection<Game> games = _gameService.GetAll(funcTest);

            _gameRepositoryMock.Verify(x => x.GetAll(funcTest), Times.Once);
        }

        [TestMethod]
        public void EditGame_ShouldCallRepositoryUpdate_Always()
        {
            _gameRepositoryMock.Setup(x => x.Update(It.IsAny<Game>()));

            _gameService.Edit(_game);

            _gameRepositoryMock.Verify(x => x.Update(_game), Times.Once);
        }

        [TestMethod]
        public void DeleteGame_ShouldCallRepositoryDelete_Always()
        {
            _gameRepositoryMock.Setup(x => x.Delete(It.IsAny<string>()));

            _gameService.Delete(_game.Id);

            _gameRepositoryMock.Verify(x => x.Delete(_game.Id), Times.Once);
        }

        [TestMethod]
        public void GetGamesByGenre_ShouldCallGetByGenreMethodInRepositoryOnce_Always()
        {
            _gameRepositoryMock.Setup(x => x.GetAllByGenre(It.IsAny<string>())).Returns(_games);

            ICollection<Game> games = _gameService.GetAllByGenre(_genre);

            _gameRepositoryMock.Verify(x => x.GetAllByGenre(_genre), Times.Once);
        }

        [TestMethod]
        public void GetGamesByGenre_ShouldCallRepositoryGetByGenreAndReturnGames_Always()
        {
            _gameRepositoryMock.Setup(x => x.GetAllByGenre(It.IsAny<string>())).Returns(_games);

            ICollection<Game> games = _gameService.GetAllByGenre(_genre);

            Assert.AreEqual(_games.Count, games.Count);
        }

        [TestMethod]
        public void GetGamesByPlatforms_ShouldCallGetByPlatformsMethodInRepositoryOnce_Always()
        {
            _gameRepositoryMock.Setup(x => x.GetAllByPlatforms(It.IsAny<ICollection<string>>())).Returns(_games);

            ICollection<Game> games = _gameService.GetAllByPlatforms(_platformNames);

            _gameRepositoryMock.Verify(x => x.GetAllByPlatforms(_platformNames), Times.Once);
        }

        [TestMethod]
        public void GetGamesByPlatforms_ShouldCallRepositoryGetByPlatformsAndReturnGames_Always()
        {
            _gameRepositoryMock.Setup(x => x.GetAllByPlatforms(It.IsAny<ICollection<string>>())).Returns(_games);

            ICollection<Game> games = _gameService.GetAllByPlatforms(_platformNames);

            Assert.AreEqual(_games.Count, games.Count);
        }

        [TestMethod]
        public void IsExist_ShouldCallRepositoryIsExistReturnFalse_WhenGameKeyIsNotFound()
        {
            _gameRepositoryMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(false);

            var result = _gameService.IsExist(_game.Key);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsExist_ShouldCallRepositoryIsExistReturnTrue_WhenGameKeyIsNotFound()
        {
            _gameRepositoryMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(true);

            var result = _gameService.IsExist(_game.Key);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAll_ShouldCallRepositoryGetAll_Always()
        {
            _gameRepositoryMock.Setup(x => x.GetAll(game => !game.IsDeleted)).Returns(_games);

            var games = _gameService.GetAll();

            Assert.AreEqual(games.Count, _games.Count);
        }

        [TestMethod]
        public void GetAll_ForFiltersCallRepositoryGetAll_BySortingCriterion()
        {
            Expression<Func<Game, bool>> expression = x => true;
            int page = 1;
            _gameRepositoryMock.Setup(x => x.GetAll(expression, x => x.Comments.Count(), false, PaginItemQuantity.Ten, page)).Returns(_games);

            var games = _gameService.GetAll(expression, SortingCriterion.MostCommented, PaginItemQuantity.Ten, page);

            Assert.AreEqual(games.Count, _games.Count);
        }

        [TestMethod]
        public void GetCountGames_ForFiltersCallRepositoryGetCountGames_Always()
        {
            Expression<Func<Game, bool>> expression = x => true;
            _gameRepositoryMock.Setup(x => x.GetCount(expression)).Returns(_games.Count);

            int countGames = _gameService.GetCountGames(expression);

            Assert.AreEqual(_games.Count, countGames);
        }
    }
}
