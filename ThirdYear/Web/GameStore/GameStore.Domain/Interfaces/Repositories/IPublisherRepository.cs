using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Repositories
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        bool IsExist(string publisherName);

        Publisher GetByName(string publisherName);

        void Delete(string publisherName);
    }
}
