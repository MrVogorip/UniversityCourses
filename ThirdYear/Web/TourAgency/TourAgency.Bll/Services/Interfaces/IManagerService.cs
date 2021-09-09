using System.Collections.Generic;
using TourAgency.Bll.BusinessModels.ResponseModel;
using TourAgency.Bll.DTO;

namespace TourAgency.Bll.Services.Interfaces
{
    public interface IManagerService : ITourService
    {
        void RegisterManager(ManagerDTO model);
        void UpdateTour(TourDTO model);
        void UpdateTourCustomer(TourCustomerDTO model);
        ListOptionModel GetListOption();
        void BlockCustomer(int id);
        void UnlockCustomer(int id);
        ManagerDTO GetManagerByIdentityUserId(string userId);
        List<CustomerDTO> GetAllCustomers();
        CustomerDTO GetCustomerById(int id);
        void ChangeDiscountCustomer(CustomerDTO customerDto);
        List<FeedbackDTO> GetActiveFeedbacks();
        void ReadFeedback(int id);
    }
}
