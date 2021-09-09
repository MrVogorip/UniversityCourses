using Ninject.Modules;
using TourAgency.Bll.Services;
using TourAgency.Bll.Services.Interfaces;

namespace TourAgency.Web.Ioc
{
    public class TourAgencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITourService>().To<TourService>();
            Bind<IManagerService>().To<ManagerService>();
            Bind<IAdminService>().To<AdminService>();
            Bind<ICustomerService>().To<CustomerService>();
        }
    }
}