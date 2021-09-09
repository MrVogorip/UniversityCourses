using GameStore.Application.Services;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Interfaces.Repositories;
using GameStore.Domain.Interfaces.Services;
using GameStore.Infrastructure.Data.Context;
using GameStore.Infrastructure.Data.Repositories;
using GameStore.Infrastructure.Data.UnitOfWork;
using GameStore.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterContextAndUoW(IServiceCollection services)
        {
            services.AddScoped<GameStoreContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<GameStoreContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IPublisherService, PublisherService>();

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IPlatformService, PlatformService>();

            services.AddScoped<IGenreService, GenreService>();

            services.AddScoped<IGameService, GameService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IInvoiceGenerateService, InvoiceGenerateService>();
        }

        public static void RegisterRepository(IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();

            services.AddScoped<IPublisherRepository, PublisherRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<IPlatformRepository, PlatformRepository>();

            services.AddScoped<IGenreRepository, GenreRepository>();

            services.AddScoped<IGameRepository, GameRepository>();
        }

        public static void RegisterLogger(IServiceCollection services)
        {
            services.AddScoped<IGameStoreLogger, GameStoreLogger>();
        }
    }
}
