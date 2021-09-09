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
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        private readonly GameStoreContext _gameStoreContext;
        private readonly IGameStoreLogger _logger;

        public PublisherRepository(GameStoreContext gameStoreContext, IGameStoreLogger logger)
            : base(gameStoreContext)
        {
            _gameStoreContext = gameStoreContext;
            _logger = logger;
        }

        public bool IsExist(string publisherName)
        {
            bool publisherIsExist = _gameStoreContext.Set<Publisher>().Any(publisher => publisher.CompanyName == publisherName);

            return publisherIsExist;
        }

        public override ICollection<Publisher> GetAll()
        {
            return _gameStoreContext.Set<Publisher>().Where(publisher => !publisher.IsDeleted).ToList();
        }

        public Publisher GetByName(string publisherName)
        {
            return _gameStoreContext.Set<Publisher>().FirstOrDefault(publisher => publisher.CompanyName == publisherName);
        }

        public void Delete(string publisherName)
        {
            Publisher publisherInDb = _gameStoreContext.Set<Publisher>()
                .FirstOrDefault(publisher => publisher.CompanyName == publisherName);

            try
            {
                if (publisherInDb is null)
                {
                    throw new ModelNotFoundInDbException("Publisher was not found in database");
                }
            }
            catch (Exception e)
            {
                _logger.LogExceptionWithParameters(e);
            }

            publisherInDb.IsDeleted = true;

            _gameStoreContext.Set<Publisher>().Update(publisherInDb);
        }
    }
}
