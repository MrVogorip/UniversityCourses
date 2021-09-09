using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Repositories
{
    public interface IPlatformRepository : IRepository<Platform>
    {
        Platform GetByName(string platformName);

        void Delete(string platformName);
    }
}
