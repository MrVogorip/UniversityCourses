using System.Collections.Generic;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Services
{
    public interface IPlatformService
    {
        void Create(Platform platform);

        ICollection<Platform> GetAll();

        ICollection<string> GetNames();

        void Edit(Platform platform);

        void Delete(string platformName);
    }
}
