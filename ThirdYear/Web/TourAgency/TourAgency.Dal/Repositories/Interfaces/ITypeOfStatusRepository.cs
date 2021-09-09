using TourAgency.Dal.Entities;

namespace TourAgency.Dal.Repositories.Interfaces
{
    public interface ITypeOfStatusRepository
    {
        TypeOfStatus Get(string type);
    }
}
