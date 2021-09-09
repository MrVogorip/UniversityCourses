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
    public class PublisherServiceTests
    {
        private PublisherService _publisherService;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IPublisherRepository> _publisherRepositoryMock;
        private Publisher _publisher;
        private ICollection<Publisher> _publishers;
        private ICollection<string> _publisherNames;

        [TestInitialize]
        public void PublisherServiceTestsInitialize()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _publisherRepositoryMock = new Mock<IPublisherRepository>();
            _publisherService = new PublisherService(_unitOfWorkMock.Object, _publisherRepositoryMock.Object);

            _publisher = new Publisher { Id = "123", CompanyName = "test name", Description = "test descrip", HomePage = "test Home Page" };
            _publishers = new List<Publisher>()
            {
                new Publisher { Id = "1", CompanyName = "test name", Description = "test descrip", HomePage = "test Home Page" },
                new Publisher { Id = "2", CompanyName = "test name", Description = "test descrip", HomePage = "test Home Page" },
            };
            _publisherNames = _publishers.Select(x => x.CompanyName).ToList();
        }

        [TestMethod]
        public void GetByName_ShouldCallRepositoryGetByName_Always()
        {
            _publisherRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(_publisher);

            var publisher = _publisherService.GetByName(_publisher.CompanyName);

            Assert.AreEqual(publisher.Id, _publisher.Id);
        }

        [TestMethod]
        public void IsExist_ShouldCallRepositoryIsExist_WhenIsFalse()
        {
            _publisherRepositoryMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(false);

            var result = _publisherService.IsExist(_publisher.CompanyName);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsExist_ShouldCallRepositoryIsExist_WhenIsTrue()
        {
            _publisherRepositoryMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(true);

            var result = _publisherService.IsExist(_publisher.CompanyName);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Create_ShouldCallRepositoryInsert_WhenPublisherIsValid()
        {
            _publisherRepositoryMock.Setup(x => x.Insert(It.IsAny<Publisher>()));

            _publisherService.Create(_publisher);

            _publisherRepositoryMock.Verify(x => x.Insert(_publisher), Times.Once);
        }

        [TestMethod]
        public void Get_ShouldCallRepositoryGet()
        {
            _publisherRepositoryMock.Setup(x => x.GetAll()).Returns(_publishers);

            var publishers = _publisherService.GetAll();

            Assert.AreEqual(_publishers.Count, publishers.Count);
        }

        [TestMethod]
        public void GetNames_ShouldCallRepositoryGet()
        {
            _publisherRepositoryMock.Setup(x => x.GetAll()).Returns(_publishers);

            var publishers = _publisherService.GetNames();

            Assert.AreEqual(_publisherNames.First(), publishers.First());
        }

        [TestMethod]
        public void Delete_ShouldCallRepositoryGetByName_Always()
        {
            _publisherRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(_publisher);

            _publisherService.Delete(_publisher.CompanyName);

            _publisherRepositoryMock.Verify(x => x.Delete(_publisher.CompanyName), Times.Once);
        }

        [TestMethod]
        public void Edit_ShouldCallRepositoryGetByName_Always()
        {
            _publisherService.Edit(_publisher);

            _publisherRepositoryMock.Verify(x => x.Update(_publisher), Times.Once);
        }
    }
}
