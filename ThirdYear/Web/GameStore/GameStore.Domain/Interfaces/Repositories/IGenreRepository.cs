using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Genre GetByName(string genreName);

        void Delete(string genreName);
    }
}
