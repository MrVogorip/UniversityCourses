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
    public class PublisherRepositoryTests
    {
        private DbContextOptions<GameStoreContext> _options;
        private PublisherRepository _publisherRepository;
        private ICollection<Publisher> _publishers;
        private Mock<IGameStoreLogger> _gameStoreLoggerMock;

        [TestInitialize]
        public void PublisherRepositoryInitialize()
        {
            _gameStoreLoggerMock = new Mock<IGameStoreLogger>();
            _options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _publishers = new List<Publisher>
            {
                new Publisher()
                {
                    Id = "3",
                    CompanyName = "test name publisher yes",
                },
                new Publisher()
                {
                    Id = "2",
                    CompanyName = "test name publisher not",
                },
            };

            using var context = new GameStoreContext(_options);
            context.Publishers.AddRange(_publishers);
            context.SaveChanges();
            _publisherRepository = new PublisherRepository(new GameStoreContext(_options), _gameStoreLoggerMock.Object);
        }

        [TestMethod]
        public void Get_ShouldCallContext_Always()
        {
            var publishers = _publisherRepository.GetAll();

            int resultCountPublishers = _publishers.Count;
            Assert.AreEqual(resultCountPublishers, publishers.Count);
        }

        [TestMethod]
        public void GetByName_ShouldCallContext_Always()
        {
            string nameTest = _publishers.First().CompanyName;

            var publisher = _publisherRepository.GetByName(nameTest);

            Assert.AreEqual(nameTest, publisher.CompanyName);
        }

        [TestMethod]
        public void IsExist_ShouldCallContext_Always()
        {
            string nameTest = _publishers.First().CompanyName;

            var result = _publisherRepository.IsExist(nameTest);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delete_ShouldCallContext_WhenPublisherIsFind()
        {
            string nameTest = _publishers.First().CompanyName;

            _publisherRepository.Delete(nameTest);
            var publisher = _publisherRepository.GetByName(nameTest);

            Assert.IsTrue(publisher.IsDeleted);
        }
    }
}
