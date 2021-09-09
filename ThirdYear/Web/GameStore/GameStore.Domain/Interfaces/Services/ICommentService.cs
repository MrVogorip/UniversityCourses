using System.Collections.Generic;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Services
{
    public interface ICommentService
    {
        void AddComment(string gameKey, Comment comment);

        ICollection<Comment> GetComments(string gameKey);

        bool IsExist(string commentId);

        void Delete(string commentId);
    }
}
