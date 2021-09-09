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
    public class GenreRepositoryTests
    {
        private DbContextOptions<GameStoreContext> _options;
        private GenreRepository _genreRepository;
        private ICollection<Genre> _genres;
        private Mock<IGameStoreLogger> _gameStoreLoggerMock;

        [TestInitialize]
        public void GenreRepositoryInitialize()
        {
            _gameStoreLoggerMock = new Mock<IGameStoreLogger>();
            _options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _genres = new List<Genre>
            {
                new Genre()
                {
                    Id = "3",
                    Name = "test name best genre",
                },
                new Genre()
                {
                    Id = "2",
                    Name = "test name worse genre",
                },
            };

            using var context = new GameStoreContext(_options);
            context.Genres.AddRange(_genres);
            context.SaveChanges();
            _genreRepository = new GenreRepository(new GameStoreContext(_options), _gameStoreLoggerMock.Object);
        }

        [TestMethod]
        public void Get_ShouldCallContext_Always()
        {
            var genres = _genreRepository.GetAll();

            int resultCountGenres = _genres.Count;
            Assert.AreEqual(resultCountGenres, genres.Count);
        }

        [TestMethod]
        public void GetByName_ShouldCallContext_Always()
        {
            string nameTest = _genres.First().Name;

            var genre = _genreRepository.GetByName(nameTest);

            Assert.AreEqual(nameTest, genre.Name);
        }

        [TestMethod]
        public void Delete_ShouldCallContext_WhenGenreIsFind()
        {
            string nameTest = _genres.First().Name;

            _genreRepository.Delete(nameTest);
            var genre = _genreRepository.GetByName(nameTest);

            Assert.IsTrue(genre.IsDeleted);
        }
    }
}
