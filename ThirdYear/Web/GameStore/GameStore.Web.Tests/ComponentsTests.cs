using System.Collections.Generic;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using GameStore.Web.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class ComponentsTests
    {
        private Mock<IGameService> _gameServiceMock;
        private TotalGames _totalGames;
        private Game _game;
        private List<Game> _games;

        [TestInitialize]
        public void ComponentsTestsInitialize()
        {
            _gameServiceMock = new Mock<IGameService>();
            _totalGames = new TotalGames(_gameServiceMock.Object);
            _game = new Game()
            {
                Id = "1",
                Description = "test descrp",
                Key = "2",
                Name = "test name",
            };
            _games = new List<Game>() { _game };
        }

        [TestMethod]
        public void Invoke_ShouldCallServiceGet_Always()
        {
            _gameServiceMock.Setup(x => x.GetCountGames()).Returns(_games.Count);

            var result = _totalGames.Invoke() as ContentViewComponentResult;

            Assert.AreEqual(result.Content, _games.Count.ToString());
        }
    }
}
