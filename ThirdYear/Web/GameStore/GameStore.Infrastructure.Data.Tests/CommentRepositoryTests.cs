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
    public class CommentRepositoryTests
    {
        private DbContextOptions<GameStoreContext> _options;
        private CommentRepository _commentRepository;
        private ICollection<Comment> _comments;
        private Mock<IGameStoreLogger> _gameStoreLoggerMock;

        [TestInitialize]
        public void CommentRepositoryInitialize()
        {
            _gameStoreLoggerMock = new Mock<IGameStoreLogger>();
            _options = new DbContextOptionsBuilder<GameStoreContext>()
                .UseInMemoryDatabase(databaseName: "PyrohovGameStore")
                .Options;

            _comments = new List<Comment>
            {
                new Comment()
                {
                    Id = "3",
                    GameId = "1",
                    Body = "test body3",
                    Name = "test name3",
                },
                new Comment()
                {
                    Id = "2",
                    GameId = "1",
                    Body = "test body2",
                    Name = "test name2",
                },
                new Comment()
                {
                    Id = "1",
                    GameId = "1",
                    Body = "test body1",
                    Name = "test name1",
                },
            };
            using var context = new GameStoreContext(_options);
            context.Comments.AddRange(_comments);
            context.SaveChanges();
            _commentRepository = new CommentRepository(new GameStoreContext(_options), _gameStoreLoggerMock.Object);
        }

        [TestMethod]
        public void GetByGameId_ShouldCallContext_Always()
        {
            string gameIdTest = _comments.First().GameId;

            var comments = _commentRepository.GetAllByGameId(gameIdTest);

            int resultCountComments = _comments.Count;
            Assert.AreEqual(resultCountComments, comments.Count);
        }
    }
}
