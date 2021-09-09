using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Models;
using GameStore.Infrastructure.Data.Context;
using GameStore.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Infrastructure.Data.Tests
{
    [TestClass]
    public class PlatformRepositoryTests
    {
        private DbContextOptions<GameStoreContext> _options;
        private PlatformRepository _platformRepository;
        private ICollection<Platform> _platforms;
        private Mock<IGameStoreLogger> _gameStoreLoggerMock;

        [TestInitialize]
        public void PlatformRepositoryInitialize()
        {
            _gameStoreLoggerMock = new Mock<IGameStoreLogger>();
            _options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _platforms = new List<Platform>
            {
                new Platform()
                {
                    Id = "3",
                    Name = "test name platform pc",
                },
                new Platform()
                {
                    Id = "2",
                    Name = "test name platform ps4",
                },
            };

            using var context = new GameStoreContext(_options);
            context.Platforms.AddRange(_platforms);
            context.SaveChanges();
            _platformRepository = new PlatformRepository(new GameStoreContext(_options), _gameStoreLoggerMock.Object);
        }

        [TestMethod]
        public void Get_ShouldCallContext_Always()
        {
            var platforms = _platformRepository.GetAll();

            int resultCountPlatforms = _platforms.Count;
            Assert.AreEqual(resultCountPlatforms, platforms.Count);
        }

        [TestMethod]
        public void GetByName_ShouldCallContext_Always()
        {
            string nameTest = _platforms.First().Name;

            var platform = _platformRepository.GetByName(nameTest);

            Assert.AreEqual(nameTest, platform.Name);
        }

        [TestMethod]
        public void Delete_ShouldCallContext_WhenPlatformIsFind()
        {
            string nameTest = _platforms.First().Name;

            _platformRepository.Delete(nameTest);
            var platform = _platformRepository.GetByName(nameTest);

            Assert.IsTrue(platform.IsDeleted);
        }
    }
}
