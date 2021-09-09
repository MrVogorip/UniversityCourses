using System;
using GameStore.Domain.Enums;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;

namespace GameStore.Application.Services
{
    public class UserService : IUserService
    {
        private static User _user1;

        public UserService()
        {
            if (_user1 is null)
            {
                _user1 = new User
                {
                    Id = "1",
                    IsBanned = false,
                    EndDateBanned = null,
                    IsDeleted = false,
                };
            }
        }

        public User GetById(string userId)
        {
            return _user1;
        }

        public bool IsExist(string userId)
        {
            return _user1.Id == userId;
        }

        public void SetBanned(User user, BanDuration banDuration)
        {
            var dateBan = DateTime.Now;
            switch (banDuration)
            {
                case BanDuration.Hour:
                    dateBan = dateBan.AddHours(1);
                    break;
                case BanDuration.Day:
                    dateBan = dateBan.AddDays(1);
                    break;
                case BanDuration.Week:
                    dateBan = dateBan.AddDays(7);
                    break;
                case BanDuration.Month:
                    dateBan = dateBan.AddMonths(1);
                    break;
                case BanDuration.Permanent:
                    dateBan = dateBan.AddYears(1000);
                    break;
                default:
                    break;
            }

            _user1.IsBanned = true;
            _user1.EndDateBanned = dateBan;
        }

        public void SetUnbanned(User user)
        {
            _user1.IsBanned = false;
            _user1.EndDateBanned = null;
        }
    }
}
