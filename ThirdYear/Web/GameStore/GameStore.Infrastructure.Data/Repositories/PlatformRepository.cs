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
    public class PlatformRepository : Repository<Platform>, IPlatformRepository
    {
        private readonly GameStoreContext _gameStoreContext;
        private readonly IGameStoreLogger _logger;

        public PlatformRepository(GameStoreContext gameStoreContext, IGameStoreLogger logger)
            : base(gameStoreContext)
        {
            _gameStoreContext = gameStoreContext;
            _logger = logger;
        }

        public override ICollection<Platform> GetAll()
        {
            return _gameStoreContext.Set<Platform>().Where(platform => !platform.IsDeleted).ToList();
        }

        public Platform GetByName(string platformName)
        {
            return _gameStoreContext.Set<Platform>().FirstOrDefault(platform => platform.Name == platformName);
        }

        public void Delete(string platformName)
        {
            Platform platformInDb = _gameStoreContext.Set<Platform>().FirstOrDefault(platform => platform.Name == platformName);

            try
            {
                if (platformInDb is null)
                {
                    throw new ModelNotFoundInDbException("Platform was not found in database");
                }
            }
            catch (Exception e)
            {
                _logger.LogExceptionWithParameters(e);
            }

            platformInDb.IsDeleted = true;

            _gameStoreContext.Set<Platform>().Update(platformInDb);
        }
    }
}
