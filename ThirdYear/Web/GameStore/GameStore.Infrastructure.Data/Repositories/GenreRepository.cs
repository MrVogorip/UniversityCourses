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
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly GameStoreContext _gameStoreContext;
        private readonly IGameStoreLogger _logger;

        public GenreRepository(GameStoreContext gameStoreContext, IGameStoreLogger logger)
            : base(gameStoreContext)
        {
            _gameStoreContext = gameStoreContext;
            _logger = logger;
        }

        public override ICollection<Genre> GetAll()
        {
            return _gameStoreContext.Set<Genre>().Where(genre => !genre.IsDeleted).ToList();
        }

        public Genre GetByName(string genreName)
        {
            return _gameStoreContext.Set<Genre>().FirstOrDefault(genre => genre.Name == genreName);
        }

        public void Delete(string genreName)
        {
            Genre genreInDb = _gameStoreContext.Set<Genre>().FirstOrDefault(genre => genre.Name == genreName);
            try
            {
                if (genreInDb is null)
                {
                    throw new ModelNotFoundInDbException("Genre was not found in database");
                }
            }
            catch (Exception e)
            {
                _logger.LogExceptionWithParameters(e);
            }

            genreInDb.IsDeleted = true;

            _gameStoreContext.Set<Genre>().Update(genreInDb);
        }
    }
}
