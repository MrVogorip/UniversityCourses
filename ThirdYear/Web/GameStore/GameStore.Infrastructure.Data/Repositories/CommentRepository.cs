using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Exceptions;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Models;
using GameStore.Infrastructure.Data.Context;
using GameStore.Logging.LoggerExtensions;

namespace GameStore.Infrastructure.Data.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly GameStoreContext _gameStoreContext;
        private readonly IGameStoreLogger _logger;

        public CommentRepository(GameStoreContext gameStoreContext, IGameStoreLogger logger)
            : base(gameStoreContext)
        {
            _gameStoreContext = gameStoreContext;
            _logger = logger;
        }

        public ICollection<Comment> GetAllByGameId(string gameId)
        {
            var commentsInDb = _gameStoreContext.Set<Comment>()
                        .Where(comment => comment.GameId == gameId)
                        .ToList();

            return commentsInDb;
        }

        public bool IsExist(string commentId)
        {
            bool commentIsExist = _gameStoreContext.Set<Comment>().Any(comment => comment.Id == commentId);

            return commentIsExist;
        }

        public void Delete(string commentId)
        {
            Comment commentInDb = _gameStoreContext.Set<Comment>().FirstOrDefault(comment => comment.Id == commentId);

            try
            {
                if (commentInDb is null)
                {
                    throw new ModelNotFoundInDbException("Comment was not found in database");
                }
            }
            catch (Exception e)
            {
                _logger.LogExceptionWithParameters(e);
            }

            commentInDb.IsDeleted = true;

            _gameStoreContext.Set<Comment>().Update(commentInDb);
        }
    }
}
