using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using GameStore.Domain.Enums;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Services
{
    public interface IGameService
    {
        bool IsExist(string gameKey);

        void Create(Game game, ICollection<string> platformNames, ICollection<string> genreNames, string companyName);

        void Edit(Game game);

        void Delete(string gameKey);

        ICollection<Game> GetAll();

        ICollection<Game> GetAll(Func<Game, bool> predicate);

        ICollection<Game> GetAll(Expression<Func<Game, bool>> expression, SortingCriterion sorting, PaginItemQuantity paginItem, int page);

        Game GetByKey(string gameKey);

        Game GetByKeyForOrder(string gameKey);

        ICollection<Game> GetAllByGenre(string genreName);

        ICollection<Game> GetAllByPlatforms(ICollection<string> platformNames);

        FileStream GetFileForDownload(string gameKey);

        int GetCountGames();

        int GetCountGames(Expression<Func<Game, bool>> expression);
    }
}
