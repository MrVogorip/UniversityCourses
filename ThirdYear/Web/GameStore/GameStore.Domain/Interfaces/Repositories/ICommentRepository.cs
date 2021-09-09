using System.Collections.Generic;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        ICollection<Comment> GetAllByGameId(string gameId);

        bool IsExist(string commentId);

        void Delete(string commentId);
    }
}
