using System.Collections.Generic;
using System.Linq;
using GameStore.Application.Services;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Application.Tests
{
    [TestClass]
    public class GenreServiceTests
    {
        private GenreService _genreService;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IGenreRepository> _genreRepositoryMock;
        private Genre _genre;
        private ICollection<Genre> _genres;

        [TestInitialize]
        public void GenreServiceTestsInitialize()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _genreService = new GenreService(_unitOfWorkMock.Object, _genreRepositoryMock.Object);
            _genre = new Genre { Id = "1", Name = "test Genre" };
            _genres = new List<Genre>()
            {
                new Genre { Name = "best genre in the world" },
                new Genre { Name = "worst genre in the world" },
            };

            _genreRepositoryMock.Setup(x => x.GetAll()).Returns(_genres);
        }

        [TestMethod]
        public void Get_ShouldCallRepositoryGet_Always()
        {
            var genres = _genreService.GetAll();

            Assert.AreEqual(genres.Count, _genres.Count);
        }

        [TestMethod]
        public void GetNames_ShouldCallRepositoryGet()
        {
            ICollection<string> genreNames = _genres.Select(x => x.Name).ToList();

            var genreNamesResult = _genreService.GetNames();

            Assert.AreEqual(genreNames.First(), genreNames.First());
        }

        [TestMethod]
        public void Create_ShouldCallRepositoryInsert_Always()
        {
            _genreRepositoryMock.Setup(x => x.Insert(It.IsAny<Genre>()));

            _genreService.Create(_genre);

            _genreRepositoryMock.Verify(x => x.Insert(_genre), Times.Once);
        }

        [TestMethod]
        public void Delete_ShouldCallRepositoryGetByName_Always()
        {
            _genreRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(_genre);

            _genreService.Delete(_genre.Name);

            _genreRepositoryMock.Verify(x => x.Delete(_genre.Name), Times.Once);
        }

        [TestMethod]
        public void Edit_ShouldCallRepositoryGetByName_Always()
        {
            _genreService.Edit(_genre);

            _genreRepositoryMock.Verify(x => x.Update(_genre), Times.Once);
        }
    }
}
