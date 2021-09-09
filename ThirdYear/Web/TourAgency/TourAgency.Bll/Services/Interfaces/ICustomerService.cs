using System.Collections.Generic;
using TourAgency.Bll.DTO;

namespace TourAgency.Bll.Services.Interfaces
{
    public interface ICustomerService : ITourService
    {
        void Register(CustomerDTO customer);
        List<TourCustomerDTO> GetTourCustomerByCustomerId(string userId);
        CustomerDTO GetCustomerByIdentityUserId(string userId);
        void BuyTour(TourDTO tour, string userId, int realNumberOfPeople, int realPrice);
        void CancelTour(TourCustomerDTO tourCustomer);
        void ChangePersonalInformation(CustomerDTO customer);
        void SendFeedback(FeedbackDTO feedbackDTO);
    }
}
