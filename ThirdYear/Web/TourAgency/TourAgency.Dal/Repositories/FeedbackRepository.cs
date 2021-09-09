using TourAgency.Dal.EF;
using TourAgency.Dal.Entities;
using TourAgency.Dal.Repositories.Interfaces;

namespace TourAgency.Dal.Repositories
{
    public class FeedbackRepository: BaseRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(TourAgencyContext context) : base(context)
        {
        }
    }
}
