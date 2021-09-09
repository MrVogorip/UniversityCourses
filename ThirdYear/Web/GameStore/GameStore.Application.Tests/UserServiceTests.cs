using GameStore.Application.Services;
using GameStore.Domain.Enums;
using GameStore.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.Application.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _userService;
        private User _user;

        [TestInitialize]
        public void UserServiceTestsInitialize()
        {
            _userService = new UserService();
            _user = new User
            {
                Id = "1",
                IsBanned = false,
                EndDateBanned = null,
                IsDeleted = false,
            };
        }

        [TestMethod]
        public void GetById_ShouldReturnModel_Always()
        {
            var user = _userService.GetById(_user.Id);

            Assert.AreEqual(user.Id, _user.Id);
        }

        [TestMethod]
        public void IsExist_ShouldReturnTrue_IfModelIsFind()
        {
            var result = _userService.IsExist(_user.Id);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SetBanned_ShouldSetBannedTrue_Always()
        {
            _userService.SetBanned(_user, BanDuration.Day);
            var user = _userService.GetById(_user.Id);

            Assert.IsTrue(user.IsBanned);
        }

        [TestMethod]
        public void SetUnbanned_ShouldSetBannedFalse_Always()
        {
            _userService.SetUnbanned(_user);
            var user = _userService.GetById(_user.Id);

            Assert.IsFalse(user.IsBanned);
        }
    }
}
