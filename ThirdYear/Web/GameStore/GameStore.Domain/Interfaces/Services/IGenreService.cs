using System.Collections.Generic;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Services
{
    public interface IGenreService
    {
        void Create(Genre genre);

        ICollection<Genre> GetAll();

        ICollection<string> GetNames();

        void Edit(Genre genre);

        void Delete(string genreName);
    }
}
