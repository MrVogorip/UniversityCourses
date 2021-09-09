using AutoMapper;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class PublisherControllerTests
    {
        private PublisherController _publisherController;
        private Mock<IPublisherService> _publisherServiceMock;
        private Mock<IMapper> _mapperMock;
        private Publisher _publisher;
        private PublisherViewModel _publisherViewModel;

        [TestInitialize]
        public void PublisherControllerTestsInitialize()
        {
            _publisherServiceMock = new Mock<IPublisherService>();
            _mapperMock = new Mock<IMapper>();
            _publisherController = new PublisherController(_publisherServiceMock.Object, _mapperMock.Object);
            _publisher = new Publisher { Id = "123", CompanyName = "test name", Description = "test descrip", HomePage = "test Home Page" };
            _publisherViewModel = new PublisherViewModel { CompanyName = "test name", Description = "test descrip", HomePage = "test Home Page" };
        }

        [TestMethod]
        public void PublisherDetails_ShouldCallPublisherServiceIsExist_WhenPublisherIsMap()
        {
            _publisherServiceMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(true);
            _publisherServiceMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(_publisher);
            _mapperMock.Setup(x => x.Map<PublisherViewModel>(It.IsAny<Publisher>())).Returns(_publisherViewModel);

            _publisherController.GetPublisherDetails(_publisher.CompanyName);

            _publisherServiceMock.Verify(x => x.GetByName(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void PublisherDetails_ShouldCallPublisherServiceIsExistFalse_ReturnNotFoundResult()
        {
            _publisherServiceMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(false);

            var result = _publisherController.GetPublisherDetails(_publisher.CompanyName);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void CreatePublisher_ShouldCallPublisherServiceCreate_WhenPublisherIsMap()
        {
            _mapperMock.Setup(x => x.Map<Publisher>(It.IsAny<PublisherViewModel>())).Returns(_publisher);
            _publisherServiceMock.Setup(x => x.Create(It.IsAny<Publisher>()));

            _publisherController.CreatePublisher(_publisherViewModel);

            _publisherServiceMock.Verify(x => x.Create(It.IsAny<Publisher>()), Times.Once);
        }
    }
}
