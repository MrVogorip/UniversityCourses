using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;

namespace GameStore.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;
        private readonly IGameRepository _gameRepository;

        public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository, IGameRepository gameRepository)
        {
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
            _gameRepository = gameRepository;
        }

        public void AddComment(string gameKey, Comment comment)
        {
            Game game = _gameRepository.GetByKey(gameKey);

            if (FindQuote(comment.Body))
            {
                comment.Body = RemoveTagQuote(comment.Body);
                comment.IsQuoted = true;
            }

            comment.GameId = game.Id;

            _commentRepository.Insert(comment);

            _unitOfWork.Commit();
        }

        public ICollection<Comment> GetComments(string gameKey)
        {
            Game game = _gameRepository.GetByKey(gameKey);

            var comments = _commentRepository.GetAllByGameId(game.Id)
                        .Where(comment => comment.ParentCommentId == null)
                        .ToList();

            return comments;
        }

        public bool IsExist(string commentId)
        {
            return _commentRepository.IsExist(commentId);
        }

        public void Delete(string commentId)
        {
            _commentRepository.Delete(commentId);

            _unitOfWork.Commit();
        }

        private bool FindQuote(string commentMessage)
        {
            if (commentMessage.IndexOf("<quote>") > -1 &&
                commentMessage.IndexOf("</quote>") > -1)
            {
                return true;
            }

            return false;
        }

        private string RemoveTagQuote(string commentMessage)
        {
            string leftQuote = "<quote>";
            string rightQuote = "</quote>";
            int indexLeftQuote = commentMessage.IndexOf(leftQuote);
            int indexRightQuote = commentMessage.IndexOf(rightQuote);

            commentMessage = commentMessage.Remove(indexLeftQuote, indexRightQuote + rightQuote.Length);

            return commentMessage;
        }
    }
}
