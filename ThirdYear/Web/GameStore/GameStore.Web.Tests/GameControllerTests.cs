using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.Domain.Enums;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModel;
using GameStore.Web.ViewModel.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class GameControllerTests
    {
        private GameController _gameController;
        private Mock<IGameService> _gameServiceMock;
        private Mock<IGenreService> _genreServiceMock;
        private Mock<IPlatformService> _platformServiceMock;
        private Mock<IPublisherService> _publisherServiceMock;
        private Mock<IMapper> _mapperMock;
        private Game _game;
        private List<Game> _games;
        private GameViewModel _gameViewModel;
        private ICollection<GameViewModel> _gameViewModels;
        private ICollection<string> _genreNames;
        private ICollection<string> _platformNames;

        [TestInitialize]
        public void GameControllerTestsInitialize()
        {
            _genreNames = new List<string>()
            {
                "test genres",
            };
            _platformNames = new List<string>()
            {
                "test platforms",
            };

            _game = new Game()
            {
                Id = "1",
                Description = "test descrp",
                Key = "2",
                Name = "test name",
            };
            _games = new List<Game>() { _game };

            _gameViewModel = new GameViewModel()
            {
                Id = "1",
                Description = "test descrp",
                Key = "2",
                Name = "test name",
                Genres = _genreNames,
                Platforms = _platformNames,
                CompanyName = "test Company Name",
                Price = 20,
                UnitsInStock = 10,
            };
            _gameViewModels = new List<GameViewModel>() { _gameViewModel };

            _gameServiceMock = new Mock<IGameService>();
            _genreServiceMock = new Mock<IGenreService>();
            _platformServiceMock = new Mock<IPlatformService>();
            _publisherServiceMock = new Mock<IPublisherService>();
            _mapperMock = new Mock<IMapper>();

            _mapperMock.Setup(x => x.Map<Game>(_gameViewModel)).Returns(_game);
            _gameServiceMock.Setup(x => x.IsExist(_game.Key)).Returns(true);

            _gameController = new GameController(_gameServiceMock.Object, _genreServiceMock.Object, _platformServiceMock.Object, _publisherServiceMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public void CreateGame_ShouldCallGameServiceCreate_WhenGameIsMap()
        {
            _mapperMock.Setup(x => x.Map<Game>(It.IsAny<GameViewModel>())).Returns(_game);
            _gameServiceMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(false);
            _gameServiceMock.Setup(x => x.Create(It.IsAny<Game>(), It.IsAny<ICollection<string>>(), It.IsAny<ICollection<string>>(), It.IsAny<string>()));

            _gameController.CreateGame(_gameViewModel);

            _gameServiceMock.Verify(x => x.Create(_game, It.IsAny<ICollection<string>>(), It.IsAny<ICollection<string>>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void EditGame_ShouldCallGameServiceCreate_WhenGameIsMap()
        {
            _gameServiceMock.Setup(x => x.Edit(It.IsAny<Game>()));

            _gameController.EditGame(_gameViewModel);

            _gameServiceMock.Verify(x => x.Edit(It.IsAny<Game>()), Times.Once);
        }

        [TestMethod]
        public void GetGameByKey_ShouldCallGameServiceGetByKey_WhenGameIsMap()
        {
            _gameServiceMock.Setup(x => x.GetByKey(It.IsAny<string>())).Returns(_game);
            _mapperMock.Setup(x => x.Map<GameViewModel>(It.IsAny<Game>())).Returns(_gameViewModel);

            _gameController.GetGameDetails(_game.Key);

            _gameServiceMock.Verify(x => x.GetByKey(_game.Key), Times.Once);
        }

        [TestMethod]
        public void GetGameByKey_ShouldCallGameServiceGetByKey_NotFoundResultWhenIsExistFalse()
        {
            _gameServiceMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(false);

            var result = _gameController.GetGameDetails(_game.Key);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void DeleteGame_ShouldCallGameServiceDelete_Always()
        {
            _gameServiceMock.Setup(x => x.Delete(It.IsAny<string>()));

            _gameController.DeleteGame(_game.Key);

            _gameServiceMock.Verify(x => x.Delete(_game.Key), Times.Once);
        }

        [TestMethod]
        public void DeleteGame_ShouldCallGameServiceDelete_NotFoundResultWhenIsExistFalse()
        {
            _gameServiceMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(false);

            var result = _gameController.DeleteGame(_game.Key);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void Download_ShouldCallGameServiceGetPathForDownload_NotFoundResultWhenIsExistFalse()
        {
            _gameServiceMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(false);

            var result = _gameController.Download(_game.Key);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void CreateGame_ShouldNewCreateGameViewModel_Always()
        {
            ViewResult result = _gameController.CreateGame() as ViewResult;
            var model = result.Model;

            Assert.IsInstanceOfType(model, typeof(CreateGameViewModel));
        }

        [TestMethod]
        public void GetGames_ShouldCallGameServiceGetAll_Always()
        {
            int page = 1;
            _gameServiceMock.Setup(x => x.GetAll(
                It.IsAny<Expression<Func<Game, bool>>>(),
                SortingCriterion.MostPopular, PaginItemQuantity.Ten, page)).Returns(_games);
            _gameServiceMock.Setup(x => x.GetCountGames(It.IsAny<Expression<Func<Game, bool>>>())).Returns(_games.Count);
            var model = new FilterAndPaginationInfoViewModel
            {
                PaginationInfo = new PaginationInfoViewModel
                {
                    PaginItemQuantity = PaginItemQuantity.Ten,
                },
            };
            var result = _gameController.GetGames(model) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(GamesAndFilterViewModel));
        }
    }
}
