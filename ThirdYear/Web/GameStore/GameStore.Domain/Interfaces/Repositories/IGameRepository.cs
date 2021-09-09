using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GameStore.Domain.Enums;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        bool IsExist(string gameKey);

        Game GetByKey(string gameKey);

        Game GetByIdForOrder(string gameId);

        Game GetByKeyForOrder(string gameKey);

        ICollection<Game> GetAll(Expression<Func<Game, bool>> filter);

        ICollection<Game> GetAll(Expression<Func<Game, bool>> filter, Expression<Func<Game, object>> sort, bool ascending, PaginItemQuantity paginItem, int page);

        ICollection<Game> GetAllByGenre(string genreName);

        ICollection<Game> GetAllByPlatforms(ICollection<string> platformNames);

        void Delete(string gameKey);

        void Create(Game game, ICollection<string> platformNames, ICollection<string> genreNames);

        int GetCount();

        int GetCount(Expression<Func<Game, bool>> expression);
    }
}
