using GameStore.Domain.Enums;
using GameStore.Domain.Models;

namespace GameStore.Domain.Interfaces.Services
{
    public interface IUserService
    {
        bool IsExist(string userId);

        User GetById(string userId);

        void SetBanned(User user, BanDuration banDuration);

        void SetUnbanned(User user);
    }
}
