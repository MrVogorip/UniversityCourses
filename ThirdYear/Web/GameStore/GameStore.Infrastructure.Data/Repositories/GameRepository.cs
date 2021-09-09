using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Enums;
using GameStore.Domain.Exceptions;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Models;
using GameStore.Infrastructure.Data.Context;
using GameStore.Logging.LoggerExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace GameStore.Infrastructure.Data.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private readonly GameStoreContext _gameStoreContext;
        private readonly IGameStoreLogger _logger;

        public GameRepository(GameStoreContext gameStoreContext, IGameStoreLogger logger)
            : base(gameStoreContext)
        {
            _gameStoreContext = gameStoreContext;
            _logger = logger;
        }

        public bool IsExist(string gameKey)
        {
            bool gameIsExist = _gameStoreContext.Set<Game>().Any(game => game.Key == gameKey);

            return gameIsExist;
        }

        public Game GetByKey(string gameKey)
        {
            return _gameStoreContext.Set<Game>()
                        .Include(game => game.Comments)
                        .Include(game => game.GameGenres)
                        .ThenInclude(gameGenres => gameGenres.Genre)
                        .Include(game => game.GamePlatforms)
                        .ThenInclude(gamePlatforms => gamePlatforms.Platform)
                        .Include(game => game.Publisher)
                        .FirstOrDefault(game => game.Key == gameKey);
        }

        public Game GetByIdForOrder(string gameId)
        {
            return _gameStoreContext.Set<Game>()
                        .Include(game => game.Publisher)
                        .FirstOrDefault(game => game.Id == gameId);
        }

        public Game GetByKeyForOrder(string gameKey)
        {
            return _gameStoreContext.Set<Game>()
                        .Include(game => game.Publisher)
                        .FirstOrDefault(game => game.Key == gameKey);
        }

        public ICollection<Game> GetAll(Expression<Func<Game, bool>> filter)
        {
            IQueryable<Game> gamesInDb = _gameStoreContext.Set<Game>().Where(x => !x.IsDeleted);

            if (filter != null)
            {
                gamesInDb = gamesInDb.AsQueryable().Where(filter);
            }

            return gamesInDb.ToList();
        }

        public ICollection<Game> GetAll(Expression<Func<Game, bool>> filter, Expression<Func<Game, object>> sort, bool ascending, PaginItemQuantity paginItem, int page)
        {
            IQueryable<Game> gamesInDb = _gameStoreContext.Set<Game>().Where(x => !x.IsDeleted);

            if (filter != null)
            {
                gamesInDb = gamesInDb.AsQueryable().Where(filter);
            }

            if (ascending)
            {
                gamesInDb = gamesInDb.AsQueryable().OrderBy(sort);
            }
            else
            {
                gamesInDb = gamesInDb.AsQueryable().OrderByDescending(sort);
            }

            gamesInDb = gamesInDb.Skip((page - 1) * (int)paginItem).Take((int)paginItem);

            return gamesInDb.ToList();
        }

        public ICollection<Game> GetAllByGenre(string genreName)
        {
            DbSet<Game> gamesInDb = _gameStoreContext.Set<Game>();
            DbSet<GameGenre> gameGenresInDb = _gameStoreContext.Set<GameGenre>();
            DbSet<Genre> genresInDb = _gameStoreContext.Set<Genre>();

            IQueryable<Game> result = from x in gamesInDb
                                      join xy in gameGenresInDb on x.Id equals xy.GameId
                                      join y in genresInDb on xy.GenreId equals y.Id
                                      where x.IsDeleted == false
                                      where y.Name == genreName
                                      select x;

            return result.Include(game => game.GameGenres)
                        .ThenInclude(gameGenres => gameGenres.Genre)
                        .ToList();
        }

        public ICollection<Game> GetAllByPlatforms(ICollection<string> platformNames)
        {
            DbSet<Game> gamesInDb = _gameStoreContext.Set<Game>();
            DbSet<GamePlatform> gamePlatformInDb = _gameStoreContext.Set<GamePlatform>();
            DbSet<Platform> platformsInDb = _gameStoreContext.Set<Platform>();

            IQueryable<Game> result = from x in gamesInDb
                                      join xy in gamePlatformInDb on x.Id equals xy.GameId
                                      join y in platformsInDb on xy.PlatformId equals y.Id
                                      where x.IsDeleted == false
                                      where platformNames.Contains(y.Name)
                                      select x;

            return result.ToList();
        }

        public void Delete(string gameKey)
        {
            Game gameInDb = _gameStoreContext.Set<Game>().FirstOrDefault(game => game.Key == gameKey);

            try
            {
                if (gameInDb is null)
                {
                    throw new ModelNotFoundInDbException("Game was not found in database");
                }
            }
            catch (Exception e)
            {
                _logger.LogExceptionWithParameters(e);
            }

            gameInDb.IsDeleted = true;

            _gameStoreContext.Set<Game>().Update(gameInDb);
        }

        public override ICollection<Game> GetAll()
        {
            return _gameStoreContext.Set<Game>()
                        .Include(game => game.Comments)
                        .Include(game => game.GameGenres)
                        .ThenInclude(gameGenres => gameGenres.Genre)
                        .Include(game => game.GamePlatforms)
                        .ThenInclude(gamePlatforms => gamePlatforms.Platform)
                        .Include(game => game.Publisher)
                        .ToList();
        }

        public override ICollection<Game> GetAll(Func<Game, bool> predicate)
        {
            return _gameStoreContext.Set<Game>()
                        .Include(game => game.Comments)
                        .Include(game => game.GameGenres)
                        .ThenInclude(gameGenres => gameGenres.Genre)
                        .Include(game => game.GamePlatforms)
                        .ThenInclude(gamePlatforms => gamePlatforms.Platform)
                        .Include(game => game.Publisher)
                        .Where(predicate)
                        .ToList();
        }

        public void Create(Game game, ICollection<string> platformNames, ICollection<string> genreNames)
        {
            var gamePlatforms = LoadGamePlatformByNames(platformNames);
            game.GamePlatforms = gamePlatforms;
            if (!(genreNames is null) && genreNames.Count != 0)
            {
                var gameGenres = LoadGameGenreByNames(genreNames);
                game.GameGenres = gameGenres;
            }

            _gameStoreContext.Set<Game>().Add(game);
        }

        public int GetCount()
        {
            return _gameStoreContext.Set<Game>().Count();
        }

        public int GetCount(Expression<Func<Game, bool>> expression)
        {
            IQueryable<Game> gamesInDb = _gameStoreContext.Set<Game>().Where(x => !x.IsDeleted);

            gamesInDb = gamesInDb.Where(expression);

            return gamesInDb.Count();
        }

        private ICollection<GameGenre> LoadGameGenreByNames(ICollection<string> genreNames)
        {
            var gameGenres = new List<GameGenre>();
            foreach (var genreName in genreNames)
            {
                Genre genre = _gameStoreContext.Set<Genre>()
                                    .FirstOrDefault(x => x.Name == genreName);

                if (genre is null)
                {
                    var newGenre = new Genre { Name = genreName };
                    _gameStoreContext.Set<Genre>().Add(newGenre);
                    var newGameGenre = new GameGenre { GenreId = newGenre.Id };
                    _gameStoreContext.Set<GameGenre>().Add(newGameGenre);

                    gameGenres.Add(newGameGenre);
                }
                else
                {
                    var newGameGenre = new GameGenre { GenreId = genre.Id };
                    _gameStoreContext.Set<GameGenre>().Add(newGameGenre);

                    gameGenres.Add(newGameGenre);
                }
            }

            return gameGenres;
        }

        private ICollection<GamePlatform> LoadGamePlatformByNames(ICollection<string> platformNames)
        {
            var gamePlatforms = new List<GamePlatform>();
            foreach (var platformName in platformNames)
            {
                Platform platform = _gameStoreContext.Set<Platform>()
                                    .FirstOrDefault(x => x.Name == platformName);

                if (platform is null)
                {
                    var newPlatform = new Platform { Name = platformName };
                    _gameStoreContext.Set<Platform>().Add(newPlatform);
                    var newGamePlatform = new GamePlatform { PlatformId = newPlatform.Id };
                    _gameStoreContext.Set<GamePlatform>().Add(newGamePlatform);

                    gamePlatforms.Add(newGamePlatform);
                }
                else
                {
                    var newGamePlatform = new GamePlatform { PlatformId = platform.Id };
                    _gameStoreContext.Set<GamePlatform>().Add(newGamePlatform);

                    gamePlatforms.Add(newGamePlatform);
                }
            }

            return gamePlatforms;
        }
    }
}
