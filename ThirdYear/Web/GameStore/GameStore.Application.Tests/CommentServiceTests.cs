using System;
using System.Collections.Generic;
using GameStore.Application.Services;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Application.Tests
{
    [TestClass]
    public class CommentServiceTests
    {
        private CommentService _commentService;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ICommentRepository> _commentRepositoryMock;
        private Mock<IGameRepository> _gameRepositoryMock;
        private Game _game;
        private Comment _comment;
        private ICollection<Comment> _comments;

        [TestInitialize]
        public void CommentServiceTestsInitialize()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _commentRepositoryMock = new Mock<ICommentRepository>();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _commentService = new CommentService(_unitOfWorkMock.Object, _commentRepositoryMock.Object, _gameRepositoryMock.Object);

            _game = new Game()
            {
                Id = "1",
                Description = "test descrp",
                Key = "2",
                Name = "test name game",
            };

            _comment = new Comment()
            {
                Id = "3",
                Body = "test body comment",
                Name = "test name comment",
            };
            _comments = new List<Comment>() { _comment };
        }

        [TestMethod]
        public void AddComment_ShouldCallInsertMethodInCommentRepositoryOnce_WhenGameIsValid()
        {
            _gameRepositoryMock.Setup(x => x.GetByKey(It.IsAny<string>())).Returns(_game);
            _commentRepositoryMock.Setup(x => x.Insert(It.IsAny<Comment>()));

            _commentService.AddComment(_game.Key, _comment);

            _commentRepositoryMock.Verify(x => x.Insert(_comment), Times.Once);
        }

        [TestMethod]
        public void GetComments_ShouldCallGameRepositoryGetByKeyAndReturnGameAndCommentRepositoryAndReturnComments_WhenGameIsNotNull()
        {
            _gameRepositoryMock.Setup(x => x.GetByKey(It.IsAny<string>())).Returns(_game);
            _commentRepositoryMock.Setup(x => x.GetAllByGameId(It.IsAny<string>())).Returns(_comments);

            ICollection<Comment> comments = _commentService.GetComments(_game.Key);

            Assert.AreEqual(_comments.Count, comments.Count);
        }

        [TestMethod]
        public void GetComments_ShouldCallGetByGameIdMethodInCommentRepositoryOnce_WhenGameIsNotNull()
        {
            _gameRepositoryMock.Setup(x => x.GetByKey(It.IsAny<string>())).Returns(_game);
            _commentRepositoryMock.Setup(x => x.GetAllByGameId(It.IsAny<string>())).Returns(_comments);

            ICollection<Comment> comments = _commentService.GetComments(_game.Key);

            _commentRepositoryMock.Verify(x => x.GetAllByGameId(_game.Id), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddComment_ShouldThrowNullReferenceException_WhenGameKeyAndCommentAreNull()
        {
            string gameKey = null;
            Comment comment = null;

            _commentService.AddComment(gameKey, comment);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddComment_ShouldThrowNullReferenceException_WhenGameIsNull()
        {
            string gameKey = null;
            Comment comment = null;
            _gameRepositoryMock.Setup(x => x.GetByKey(It.IsAny<string>())).Returns(_game);

            _commentService.AddComment(gameKey, comment);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetComments_ShouldThrowNullReferenceException_WhenGameKeyIsNull()
        {
            _commentService.GetComments(null);
        }

        [TestMethod]
        public void DeleteComment_ShouldCallCommentRepositoryFindAndUpdate_Always()
        {
            _commentService.Delete(_comment.Id);

            _commentRepositoryMock.Verify(x => x.Delete(_comment.Id), Times.Once);
        }

        [TestMethod]
        public void IsExist_ShouldCallCommentRepositoryIsExist_Always()
        {
            _commentService.IsExist(_comment.Id);

            _commentRepositoryMock.Verify(x => x.IsExist(_comment.Id), Times.Once);
        }
    }
}
