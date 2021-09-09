using System.Linq;
using AutoMapper;
using GameStore.Domain.Models;
using GameStore.Web.ViewModel;
using GameStore.Web.ViewModel.Comment;
using GameStore.Web.ViewModel.Game;
using GameStore.Web.ViewModel.Order;
using GameStore.Web.ViewModel.User;

namespace GameStore.Web.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            CreateMap<Comment, CommentViewModel>().ReverseMap();

            CreateMap<Genre, GenreViewModel>().ReverseMap();

            CreateMap<Platform, PlatformViewModel>().ReverseMap();

            CreateMap<Publisher, PublisherViewModel>().ReverseMap();

            CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.Status, xy => xy.MapFrom(y => y.Status.ToString()));

            CreateMap<OrderViewModel, Order>()
                .ForMember(x => x.Status, xy => xy.Ignore());

            CreateMap<OrderDetails, OrderDetailsViewModel>().ReverseMap();

            CreateMap<Game, GameViewModel>()
                .ForMember(x => x.Platforms, xy => xy.MapFrom(y => y.GamePlatforms.Select(z => z.Platform.Name).ToList()))
                .ForMember(x => x.Genres, xy => xy.MapFrom(y => y.GameGenres.Select(z => z.Genre.Name).ToList()))
                .ForMember(x => x.CompanyName, xy => xy.MapFrom(y => y.Publisher.CompanyName));

            CreateMap<GameViewModel, Game>()
                .ForMember(x => x.GamePlatforms, xy => xy.Ignore())
                .ForMember(x => x.GameGenres, xy => xy.Ignore())
                .ForMember(x => x.Publisher, xy => xy.Ignore());

            CreateMap<GameViewModel, CreateGameViewModel>()
                .ForMember(x => x.Platforms, xy => xy.Ignore())
                .ForMember(x => x.Genres, xy => xy.Ignore())
                .ForMember(x => x.CompanyNames, xy => xy.Ignore());

            CreateMap<OrderViewModel, ConfirmOrderViewModel>()
                .ForMember(x => x.CardHoldersName, xy => xy.Ignore())
                .ForMember(x => x.CardNumber, xy => xy.Ignore())
                .ForMember(x => x.ExpirationDate, xy => xy.Ignore())
                .ForMember(x => x.CardVerificationValue, xy => xy.Ignore())
                .ReverseMap();

            CreateMap<User, UserViewModel>()
                .ReverseMap();

            CreateMap<UserViewModel, BanUserViewModel>()
                .ForMember(x => x.BanDuration, xy => xy.Ignore())
                .ReverseMap();
        }
    }
}
