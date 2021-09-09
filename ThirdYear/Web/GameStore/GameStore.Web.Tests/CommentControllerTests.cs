using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using GameStore.Web.Controllers;
using GameStore.Web.ViewModel.Comment;
using GameStore.Web.ViewModel.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class CommentControllerTests
    {
        private CommentController _commentController;
        private Mock<ICommentService> _commentServiceMock;
        private Mock<IGameService> _gameServiceMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IMapper> _mapperMock;
        private Comment _comment;
        private ICollection<Comment> _comments;
        private CommentViewModel _commentViewModel;
        private ICollection<CommentViewModel> _commentViewModels;
        private string gameKey;
        private Game _game;
        private GameViewModel _gameViewModel;

        [TestInitialize]
        public void CommentControllerTestsInitialize()
        {
            gameKey = "1";
            _commentServiceMock = new Mock<ICommentService>();
            _gameServiceMock = new Mock<IGameService>();
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();

            _gameServiceMock.Setup(x => x.IsExist(gameKey)).Returns(true);
            _commentController = new CommentController(_gameServiceMock.Object, _commentServiceMock.Object, _userServiceMock.Object, _mapperMock.Object);

            _comment = new Comment()
            {
                Id = "3",
                Body = "test body comment",
                Name = "test name comment",
            };
            _comments = new List<Comment>() { _comment };

            _commentViewModel = new CommentViewModel()
            {
                Body = "test body comment",
                Name = "test name comment",
            };
            _commentViewModels = new List<CommentViewModel>() { _commentViewModel };

            _game = new Game()
            {
                Id = "1",
                Description = "test descrp",
                Key = "2",
                Name = "test name game",
            };
            _gameViewModel = new GameViewModel()
            {
                Description = "test descrp",
                Key = "2",
                Name = "test name",
                CompanyName = "test Company Name",
            };
        }

        [TestMethod]
        public void LeaveComment_ShouldCallCommentServiceAddComment_WhenCommentIsMap()
        {
            _mapperMock.Setup(x => x.Map<Comment>(It.IsAny<CommentViewModel>())).Returns(_comment);
            _commentServiceMock.Setup(x => x.AddComment(It.IsAny<string>(), It.IsAny<Comment>()));

            _commentController.LeaveComment(gameKey, _commentViewModel);

            _commentServiceMock.Verify(x => x.AddComment(gameKey, _comment), Times.Once);
        }

        [TestMethod]
        public void LeaveComment_ShouldCallGameServiceGetByKey_NotFoundResultWhenIsExistFalse()
        {
            _gameServiceMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(false);

            var result = _commentController.LeaveComment(gameKey, _commentViewModel);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void GetComments_ShouldCallCommentServiceGetComments_WhenCommentIsMap()
        {
            _mapperMock.Setup(x => x.Map<GameViewModel>(It.IsAny<Game>())).Returns(_gameViewModel);
            _mapperMock.Setup(x => x.Map<ICollection<CommentViewModel>>(It.IsAny<ICollection<Comment>>())).Returns(_commentViewModels);
            _gameServiceMock.Setup(x => x.GetByKey(It.IsAny<string>())).Returns(_game);
            _commentServiceMock.Setup(x => x.GetComments(It.IsAny<string>())).Returns(_comments);

            _commentController.GetComments(gameKey, true);

            _commentServiceMock.Verify(x => x.GetComments(gameKey), Times.Once);
        }

        [TestMethod]
        public void GetComments_ShouldCallGameServiceGetByKey_NotFoundResultWhenIsExistFalse()
        {
            _gameServiceMock.Setup(x => x.IsExist(It.IsAny<string>())).Returns(false);

            var result = _commentController.GetComments(gameKey, true);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void GetComments_ShouldCallGameServiceGetByKey_NotFoundResultWhenIsCommentsNull()
        {
            _mapperMock.Setup(x => x.Map<GameViewModel>(It.IsAny<Game>())).Returns(_gameViewModel);
            _mapperMock.Setup(x => x.Map<ICollection<CommentViewModel>>(It.IsAny<ICollection<Comment>>())).Returns(_commentViewModels);
            _gameServiceMock.Setup(x => x.GetByKey(It.IsAny<string>())).Returns(_game);

            var result = _commentController.GetComments(gameKey, true);

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void GetComments_ShouldCallGameServiceGetByKey_ViewResultReturn()
        {
            _mapperMock.Setup(x => x.Map<GameViewModel>(It.IsAny<Game>())).Returns(_gameViewModel);
            _mapperMock.Setup(x => x.Map<ICollection<CommentViewModel>>(It.IsAny<ICollection<Comment>>())).Returns(_commentViewModels);
            _gameServiceMock.Setup(x => x.GetByKey(It.IsAny<string>())).Returns(_game);
            _commentServiceMock.Setup(x => x.GetComments(It.IsAny<string>())).Returns(new List<Comment>());

            var result = _commentController.GetComments(gameKey, true);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Delete_ShouldCallGameServiceDelete_ViewResultReturn()
        {
            _commentServiceMock.Setup(x => x.GetComments(gameKey)).Returns(_comments);
            _commentServiceMock.Setup(x => x.IsExist(_comment.Id)).Returns(true);
            _gameServiceMock.Setup(x => x.IsExist(gameKey)).Returns(true);
            _gameServiceMock.Setup(x => x.GetByKey(gameKey)).Returns(_game);

            _commentController.Delete(gameKey, _comment.Id);

            _commentServiceMock.Verify(x => x.Delete(_comment.Id), Times.Once);
        }
    }
}
