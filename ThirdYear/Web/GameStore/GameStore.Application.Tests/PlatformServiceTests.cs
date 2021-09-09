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
    public class PlatformServiceTests
    {
        private PlatformService _platformService;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IPlatformRepository> _platformRepositoryMock;
        private Platform _platform;
        private ICollection<Platform> _platforms;
        private ICollection<string> _platformNames;

        [TestInitialize]
        public void GenreServiceTestsInitialize()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _platformRepositoryMock = new Mock<IPlatformRepository>();
            _platformService = new PlatformService(_unitOfWorkMock.Object, _platformRepositoryMock.Object);
            _platform = new Platform { Id = "1", Name = "test name platform" };
            _platforms = new List<Platform>()
            {
                new Platform { Name = "best platform in the world" },
                new Platform { Name = "worst platform in the world" },
            };
            _platformNames = _platforms.Select(x => x.Name).ToList();
        }

        [TestMethod]
        public void Get_ShouldCallRepositoryGet()
        {
            _platformRepositoryMock.Setup(x => x.GetAll()).Returns(_platforms);

            var platforms = _platformService.GetAll();

            Assert.AreEqual(platforms.Count, _platforms.Count);
        }

        [TestMethod]
        public void GetNames_ShouldCallRepositoryGet_Always()
        {
            _platformRepositoryMock.Setup(x => x.GetAll()).Returns(_platforms);

            var platformNames = _platformService.GetNames();

            Assert.AreEqual(platformNames.Count, _platformNames.Count);
        }

        [TestMethod]
        public void Create_ShouldCallRepositoryInsert_WhenIsValid()
        {
            _platformRepositoryMock.Setup(x => x.Insert(It.IsAny<Platform>()));

            _platformService.Create(_platform);

            _platformRepositoryMock.Verify(x => x.Insert(_platform), Times.Once);
        }

        [TestMethod]
        public void Delete_ShouldCallRepositoryGetByName_Always()
        {
            _platformRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns(_platform);

            _platformService.Delete(_platform.Name);

            _platformRepositoryMock.Verify(x => x.Delete(_platform.Name), Times.Once);
        }

        [TestMethod]
        public void Edit_ShouldCallRepositoryGetByName_Always()
        {
            _platformService.Edit(_platform);

            _platformRepositoryMock.Verify(x => x.Update(_platform), Times.Once);
        }
    }
}
