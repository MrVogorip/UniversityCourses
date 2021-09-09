namespace TourAgency.Dal.Repositories.Interfaces
{
    public interface IManagerRepository
    {
        int GetCManagerIdByIdentityUserId(string userId);
    }
}
