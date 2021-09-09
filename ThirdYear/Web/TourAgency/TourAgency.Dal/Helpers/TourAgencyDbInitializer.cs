 using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using TourAgency.Dal.EF;
using TourAgency.Dal.Entities;

namespace TourAgency.Dal.Helpers
{
    public class TourAgencyDbInitializer : DropCreateDatabaseAlways<TourAgencyContext>
    {

        //add db 
        protected override void Seed(TourAgencyContext db)
        {
            // add role
            var adminRole = new IdentityRole() { Name = "admin" };
            var managerRole = new IdentityRole() { Name = "manager" };
            var customerRole = new IdentityRole() { Name = "customer" };
            db.Roles.Add(adminRole);
            db.Roles.Add(managerRole);
            db.Roles.Add(customerRole);
            // add admin and manager
            var adminUser = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                PasswordHash = new PasswordHasher().HashPassword("qwe123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var managerUser = new IdentityUser
            {
                UserName = "manager@gmail.com",
                Email = "manager@gmail.com",
                PasswordHash = new PasswordHasher().HashPassword("asd123"),
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var customerUser1 = new IdentityUser { UserName = "customer1@gmail.com", Email = "customer1@gmail.com", PasswordHash = new PasswordHasher().HashPassword("zxc123"), SecurityStamp = Guid.NewGuid().ToString(), };
            var customerUser2 = new IdentityUser { UserName = "customer2@gmail.com", Email = "customer2@gmail.com", PasswordHash = new PasswordHasher().HashPassword("zxc123"), SecurityStamp = Guid.NewGuid().ToString(), };
            var customerUser3 = new IdentityUser { UserName = "customer3@gmail.com", Email = "customer3@gmail.com", PasswordHash = new PasswordHasher().HashPassword("zxc123"), SecurityStamp = Guid.NewGuid().ToString(), };
            var customerUser4 = new IdentityUser { UserName = "customer4@gmail.com", Email = "customer4@gmail.com", PasswordHash = new PasswordHasher().HashPassword("zxc123"), SecurityStamp = Guid.NewGuid().ToString(), };
            var customerUser5 = new IdentityUser { UserName = "customer5@gmail.com", Email = "customer5@gmail.com", PasswordHash = new PasswordHasher().HashPassword("zxc123"), SecurityStamp = Guid.NewGuid().ToString(), };
            var customerUser6 = new IdentityUser { UserName = "customer6@gmail.com", Email = "customer6@gmail.com", PasswordHash = new PasswordHasher().HashPassword("zxc123"), SecurityStamp = Guid.NewGuid().ToString(), };
            var customerUser7 = new IdentityUser { UserName = "customer7@gmail.com", Email = "customer7@gmail.com", PasswordHash = new PasswordHasher().HashPassword("zxc123"), SecurityStamp = Guid.NewGuid().ToString(), };
            var customerUser8 = new IdentityUser { UserName = "customer8@gmail.com", Email = "customer8@gmail.com", PasswordHash = new PasswordHasher().HashPassword("zxc123"), SecurityStamp = Guid.NewGuid().ToString(), };
            var customerUser9 = new IdentityUser { UserName = "customer9@gmail.com", Email = "customer9@gmail.com", PasswordHash = new PasswordHasher().HashPassword("zxc123"), SecurityStamp = Guid.NewGuid().ToString(), };

            db.Users.Add(adminUser);
            db.Users.Add(managerUser);

            db.Users.Add(customerUser1);
            db.Users.Add(customerUser2);
            db.Users.Add(customerUser3);
            db.Users.Add(customerUser4);
            db.Users.Add(customerUser5);
            db.Users.Add(customerUser6);
            db.Users.Add(customerUser7);
            db.Users.Add(customerUser8);
            db.Users.Add(customerUser9);

            // add roles for staff
            var adminUserRole = new IdentityUserRole
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id,
            };
            var managerUserRole = new IdentityUserRole
            {
                UserId = managerUser.Id,
                RoleId = managerRole.Id,
            };
            var customer1UserRole = new IdentityUserRole { UserId = customerUser1.Id, RoleId = customerRole.Id, };
            var customer2UserRole = new IdentityUserRole { UserId = customerUser2.Id, RoleId = customerRole.Id, };
            var customer3UserRole = new IdentityUserRole { UserId = customerUser3.Id, RoleId = customerRole.Id, };
            var customer4UserRole = new IdentityUserRole { UserId = customerUser4.Id, RoleId = customerRole.Id, };
            var customer5UserRole = new IdentityUserRole { UserId = customerUser5.Id, RoleId = customerRole.Id, };
            var customer6UserRole = new IdentityUserRole { UserId = customerUser6.Id, RoleId = customerRole.Id, };
            var customer7UserRole = new IdentityUserRole { UserId = customerUser7.Id, RoleId = customerRole.Id, };
            var customer8UserRole = new IdentityUserRole { UserId = customerUser8.Id, RoleId = customerRole.Id, };
            var customer9UserRole = new IdentityUserRole { UserId = customerUser9.Id, RoleId = customerRole.Id, };
            db.SaveChanges();
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{adminUserRole.UserId}','{adminUserRole.RoleId}')");
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{managerUserRole.UserId}','{managerUserRole.RoleId}')");
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{customer1UserRole.UserId}','{customer1UserRole.RoleId}')");
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{customer2UserRole.UserId}','{customer2UserRole.RoleId}')");
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{customer3UserRole.UserId}','{customer3UserRole.RoleId}')");
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{customer4UserRole.UserId}','{customer4UserRole.RoleId}')");
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{customer5UserRole.UserId}','{customer5UserRole.RoleId}')");
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{customer6UserRole.UserId}','{customer6UserRole.RoleId}')");
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{customer7UserRole.UserId}','{customer7UserRole.RoleId}')");
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{customer8UserRole.UserId}','{customer8UserRole.RoleId}')");
            db.Database.ExecuteSqlCommand($"INSERT INTO AspNetUserRoles (UserId,RoleId) VALUES ('{customer9UserRole.UserId}','{customer9UserRole.RoleId}')");
            db.SaveChanges();
            var admin = new Administrator()
            {
                Id = 1,
                Name = "Name 1",
                Surname = "Surname 1",
                UserId = adminUser.Id,
                User = adminUser,
            };
            db.Administrators.Add(admin);
            var manager = new Manager()
            {
                Id = 1,
                Name = "Name 2",
                Surname = "Surname 2",
                UserId = managerUser.Id,
                User = managerUser,
                IsBlock = false
            };
            db.Managers.Add(manager);
            var customer1 = new Customer() { Id = 1, Name = "Tobias", Surname = "Rogers", UserId = customerUser1.Id, User = customerUser1, IsBlock = false, Discount = 14, MaxDiscount = 50, StepDiscount = 2, };
            var customer2 = new Customer() { Id = 1, Name = "Harris", Surname = "Young", UserId = customerUser2.Id, User = customerUser2, IsBlock = false, Discount = 0, MaxDiscount = 50, StepDiscount = 5, };
            var customer3 = new Customer() { Id = 1, Name = "Salem", Surname = "Diaz", UserId = customerUser3.Id, User = customerUser3, IsBlock = false, Discount = 0, MaxDiscount = 50, StepDiscount = 5, };
            var customer4 = new Customer() { Id = 1, Name = "Zahir", Surname = "Hayes", UserId = customerUser4.Id, User = customerUser4, IsBlock = false, Discount = 0, MaxDiscount = 50, StepDiscount = 5, };
            var customer5 = new Customer() { Id = 1, Name = "Wolfgang", Surname = "Turner", UserId = customerUser5.Id, User = customerUser5, IsBlock = false, Discount = 0, MaxDiscount = 50, StepDiscount = 5, };
            var customer6 = new Customer() { Id = 1, Name = "Roman", Surname = "Patterson", UserId = customerUser6.Id, User = customerUser6, IsBlock = false, Discount = 0, MaxDiscount = 50, StepDiscount = 5, };
            var customer7 = new Customer() { Id = 1, Name = "Neil", Surname = "Thomas", UserId = customerUser7.Id, User = customerUser7, IsBlock = false, Discount = 0, MaxDiscount = 50, StepDiscount = 5, };
            var customer8 = new Customer() { Id = 1, Name = "Graham", Surname = "Ward", UserId = customerUser8.Id, User = customerUser8, IsBlock = false, Discount = 0, MaxDiscount = 50, StepDiscount = 5, };
            var customer9 = new Customer() { Id = 1, Name = "Jameson", Surname = "Hayes", UserId = customerUser9.Id, User = customerUser9, IsBlock = false, Discount = 0, MaxDiscount = 50, StepDiscount = 5, };

            db.Customers.Add(customer1);
            db.Customers.Add(customer2);
            db.Customers.Add(customer3);
            db.Customers.Add(customer4);
            db.Customers.Add(customer5);
            db.Customers.Add(customer6);
            db.Customers.Add(customer7);
            db.Customers.Add(customer8);
            db.Customers.Add(customer9);
            db.SaveChanges();

            var cityKyiv = new City() { Id = 1, NameCity = "Kyiv", };
            var cityVienna = new City() { Id = 2, NameCity = "Vienna", };
            var cityLondon = new City() { Id = 3, NameCity = "London", };
            var cityBudapest = new City() { Id = 4, NameCity = "Budapest", };
            var cityBerlin = new City() { Id = 5, NameCity = "Berlin", };
            var cityRome = new City() { Id = 6, NameCity = "Rome", };
            var cityMoscow = new City() { Id = 7, NameCity = "Moscow", };
            var cityStockholm = new City() { Id = 8, NameCity = "Stockholm", };
            db.Cities.Add(cityKyiv);
            db.Cities.Add(cityVienna);
            db.Cities.Add(cityLondon);
            db.Cities.Add(cityBudapest);
            db.Cities.Add(cityBerlin);
            db.Cities.Add(cityRome);
            db.Cities.Add(cityMoscow);
            db.Cities.Add(cityStockholm);

            var typeOfHotelHostel = new TypeOfHotel
            {
                Id = 1,
                NumberOfStars = 4,
                Type = "Hostel",
                IsDelete = false,
            };
            var typeOfHotelBusiness = new TypeOfHotel
            {
                Id = 2,
                NumberOfStars = 5,
                Type = "Business",
                IsDelete = false
            };
            var typeOfHotelBedandBreakfast = new TypeOfHotel
            {
                Id = 3,
                NumberOfStars = 3,
                Type = "Bed and Breakfast",
                IsDelete = false
            };
            var typeOfHotelLodge = new TypeOfHotel
            {
                Id = 4,
                NumberOfStars = 2,
                Type = "Lodge",
                IsDelete = false
            };
            db.TypeOfHotels.Add(typeOfHotelHostel);
            db.TypeOfHotels.Add(typeOfHotelBusiness);
            db.TypeOfHotels.Add(typeOfHotelBedandBreakfast);
            db.TypeOfHotels.Add(typeOfHotelLodge);

            var typeOfTourRelax = new TypeOfTour { Id = 1, Type = "Relax" };
            var typeOfTourExcursion = new TypeOfTour { Id = 2, Type = "Excursion" };
            var typeOfTourShopping = new TypeOfTour { Id = 3, Type = "Shopping" };
            var typeOfTourSport = new TypeOfTour { Id = 4, Type = "Sport" };
            db.TypeOfTours.Add(typeOfTourRelax);
            db.TypeOfTours.Add(typeOfTourExcursion);
            db.TypeOfTours.Add(typeOfTourShopping);
            db.TypeOfTours.Add(typeOfTourSport);

            var typeOfStatusRegistered = new TypeOfStatus() { Id = 1, Type = "Registered" };
            var typeOfStatusPaid = new TypeOfStatus() { Id = 2, Type = "Paid" };
            var typeOfStatusCanceled = new TypeOfStatus() { Id = 3, Type = "Canceled" };

            db.TypeOfStatuses.Add(typeOfStatusRegistered);
            db.TypeOfStatuses.Add(typeOfStatusPaid);
            db.TypeOfStatuses.Add(typeOfStatusCanceled);

            var tour1 = new Tour()
            {
                Id = 1,
                City = cityKyiv,
                CityId = cityKyiv.Id,
                Price = 2000,
                IsHot = false,
                StartOfTour = Convert.ToDateTime("04/05/2020"),
                EndOfTour = Convert.ToDateTime("04/06/2020"),
                MaxNumberOfPeople = 10,
                TypeOfHotel = typeOfHotelHostel,
                TypeOfHotelId = typeOfHotelHostel.Id,
                TypeOfTour = typeOfTourRelax,
                TypeOfTourId = typeOfTourRelax.Id,
                IsDelete = false,
                NumberOfOrders = 3,
                ImagePath = "Kyiv.jpg"
            };
            var tour2 = new Tour()
            {
                Id = 2,
                City = cityRome,
                CityId = cityRome.Id,
                Price = 4400,
                IsHot = true,
                StartOfTour = Convert.ToDateTime("18/05/2020"),
                EndOfTour = Convert.ToDateTime("18/06/2020"),
                MaxNumberOfPeople = 3,
                TypeOfHotel = typeOfHotelBedandBreakfast,
                TypeOfHotelId = typeOfHotelBedandBreakfast.Id,
                TypeOfTour = typeOfTourShopping,
                TypeOfTourId = typeOfTourShopping.Id,
                IsDelete = false,
                NumberOfOrders = 2,
                ImagePath = "Rome.jpg"
            };
            var tour3 = new Tour()
            {
                Id = 3,
                City = cityLondon,
                CityId = cityLondon.Id,
                Price = 3800,
                IsHot = false,
                StartOfTour = Convert.ToDateTime("12/05/2020"),
                EndOfTour = Convert.ToDateTime("12/06/2020"),
                MaxNumberOfPeople = 5,
                TypeOfHotel = typeOfHotelLodge,
                TypeOfHotelId = typeOfHotelLodge.Id,
                TypeOfTour = typeOfTourExcursion,
                TypeOfTourId = typeOfTourExcursion.Id,
                IsDelete = false,
                NumberOfOrders = 3,
                ImagePath = "London.jpg"
            };
            var tour4 = new Tour()
            {
                Id = 4,
                City = cityBerlin,
                CityId = cityBerlin.Id,
                Price = 8500,
                IsHot = false,
                StartOfTour = Convert.ToDateTime("04/07/2020"),
                EndOfTour = Convert.ToDateTime("04/08/2020"),
                MaxNumberOfPeople = 10,
                TypeOfHotel = typeOfHotelHostel,
                TypeOfHotelId = typeOfHotelHostel.Id,
                TypeOfTour = typeOfTourRelax,
                TypeOfTourId = typeOfTourRelax.Id,
                IsDelete = false,
                NumberOfOrders = 5,
                ImagePath = "Berlin.jpg"
            };
            var tour5 = new Tour()
            {
                Id = 5,
                City = cityMoscow,
                CityId = cityMoscow.Id,
                Price = 7200,
                IsHot = true,
                StartOfTour = Convert.ToDateTime("09/06/2020"),
                EndOfTour = Convert.ToDateTime("10/06/2020"),
                MaxNumberOfPeople = 7,
                TypeOfHotel = typeOfHotelHostel,
                TypeOfHotelId = typeOfHotelHostel.Id,
                TypeOfTour = typeOfTourRelax,
                TypeOfTourId = typeOfTourRelax.Id,
                IsDelete = false,
                NumberOfOrders = 3,
                ImagePath = "Moscow.jpeg"
            };
            var tour6 = new Tour()
            {
                Id = 6,
                City = cityStockholm,
                CityId = cityStockholm.Id,
                Price = 6300,
                IsHot = false,
                StartOfTour = Convert.ToDateTime("19/07/2020"),
                EndOfTour = Convert.ToDateTime("27/07/2020"),
                MaxNumberOfPeople = 3,
                TypeOfHotel = typeOfHotelLodge,
                TypeOfHotelId = typeOfHotelLodge.Id,
                TypeOfTour = typeOfTourShopping,
                TypeOfTourId = typeOfTourShopping.Id,
                IsDelete = false,
                NumberOfOrders = 2,
                ImagePath = "Stockholm.jpg"
            };
            db.Tours.Add(tour1);
            db.Tours.Add(tour2);
            db.Tours.Add(tour3);
            db.Tours.Add(tour4);
            db.Tours.Add(tour5);
            db.Tours.Add(tour6);
            var tourCustomer1 = new TourCustomer()
            {
                Id = 1,
                Customer = customer1,
                CustomerId = customer1.Id,
                Tour = tour1,
                TourId = tour1.Id,
                TypeOfStatus = typeOfStatusRegistered,
                TypeOfStatusId = typeOfStatusRegistered.Id,
                RealNumberOfPeople = 2,
                RealPrice = 1800,
            };
            var tourCustomer2 = new TourCustomer()
            {
                Id = 2,
                Customer = customer1,
                CustomerId = customer1.Id,
                Tour = tour4,
                TourId = tour4.Id,
                TypeOfStatus = typeOfStatusRegistered,
                TypeOfStatusId = typeOfStatusRegistered.Id,
                RealNumberOfPeople = 3,
                RealPrice = 8000
            };
            db.TourCustomers.Add(tourCustomer1);
            db.TourCustomers.Add(tourCustomer2);
            db.SaveChanges();


        }
    }
}
