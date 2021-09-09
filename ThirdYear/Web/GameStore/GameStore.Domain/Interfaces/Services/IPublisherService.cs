using System.Collections.Generic;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Services
{
    public interface IPublisherService
    {
        Publisher GetByName(string publisherName);

        bool IsExist(string publisherName);

        void Create(Publisher publisher);

        ICollection<Publisher> GetAll();

        ICollection<string> GetNames();

        void Edit(Publisher publisher);

        void Delete(string publisherName);
    }
}
