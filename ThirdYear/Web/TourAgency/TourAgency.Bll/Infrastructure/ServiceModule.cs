using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourAgency.Dal.UnitOfWork;
using TourAgency.Dal.UnitOfWork.Interfaces;

namespace TourAgency.Bll.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string _connectionString;
        public ServiceModule(string connection)
        {
            _connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(_connectionString);
        }
    }
}
