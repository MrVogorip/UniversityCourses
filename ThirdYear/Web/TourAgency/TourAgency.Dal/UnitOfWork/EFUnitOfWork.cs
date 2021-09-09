using System;
using TourAgency.Dal.EF;
using TourAgency.Dal.Repositories;
using TourAgency.Dal.UnitOfWork.Interfaces;

namespace TourAgency.Dal.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly TourAgencyContext _context;

        private AdministratorRepository _administratorRepository;
        private CityRepository _cityRepository;
        private CustomerRepository _customerRepository;
        private ManagerRepository _managerRepository;
        private TourRepository _tourRepository;
        private TourCustomersRepository _tourCustomersRepository;
        private TypeOfHotelRepository _typeOfHotelRepository;
        private TypeOfStatusRepository _typeOfStatuslRepository;
        private TypeOfTourRepository _typeOfTourRepository;
        private FeedbackRepository _feedbackRepository;

        private bool _disposed = false;

        public EFUnitOfWork(string connectionString)
        {
            _context = new TourAgencyContext(connectionString);
        }
        public AdministratorRepository Administrators
        {
            get
            {
                if (_administratorRepository == null)
                    _administratorRepository = new AdministratorRepository(_context);
                return _administratorRepository;
            }
        }
        public CityRepository Cites
        {
            get
            {
                if (_cityRepository == null)
                    _cityRepository = new CityRepository(_context);
                return _cityRepository;
            }
        }
        public CustomerRepository Customers
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_context);
                return _customerRepository;
            }
        }
        public ManagerRepository Managers
        {
            get
            {
                if (_managerRepository == null)
                    _managerRepository = new ManagerRepository(_context);
                return _managerRepository;
            }
        }
        public TourRepository Tours
        {
            get
            {
                if (_tourRepository == null)
                    _tourRepository = new TourRepository(_context);
                return _tourRepository;
            }
        }
        public TypeOfHotelRepository TypeOfHotels
        {
            get
            {
                if (_typeOfHotelRepository == null)
                    _typeOfHotelRepository = new  TypeOfHotelRepository(_context);
                return _typeOfHotelRepository;
            }
        }
        public TypeOfTourRepository TypeOfTours
        {
            get
            {
                if (_typeOfTourRepository == null)
                    _typeOfTourRepository = new TypeOfTourRepository(_context);
                return _typeOfTourRepository;
            }
        }

        public TourCustomersRepository TourCustomers
        {
            get
            {
                if (_tourCustomersRepository == null)
                    _tourCustomersRepository = new  TourCustomersRepository(_context);
                return _tourCustomersRepository;
            }
        }

        public TypeOfStatusRepository TypeOfStatuses
        {
            get
            {
                if (_typeOfStatuslRepository == null)
                    _typeOfStatuslRepository = new TypeOfStatusRepository(_context);
                return _typeOfStatuslRepository;
            }
        }
        public FeedbackRepository Feedbacks
        {
            get
            {
                if (_feedbackRepository == null)
                    _feedbackRepository = new FeedbackRepository(_context);
                return _feedbackRepository;
            }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this._disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
