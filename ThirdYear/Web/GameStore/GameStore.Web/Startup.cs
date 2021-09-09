using AutoMapper;
using GameStore.Infrastructure.Data.Context;
using GameStore.Infrastructure.IoC;
using GameStore.Web.Filters;
using GameStore.Web.Helpers;
using GameStore.Web.Middlewares;
using GameStore.Web.Payments;
using GameStore.Web.Payments.Interfaces;
using GameStore.Web.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GameStore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddDbContext<GameStoreContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString(ResourceNames.PathResourceJson.DefaultConnection)));
            }
            catch (SqlException)
            {
                throw;
            }

            SetResourceNames();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews(options =>
                options.Filters.Add(typeof(LoggerFilter)))
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddMemoryCache();

            DependencyContainer.RegisterContextAndUoW(services);
            DependencyContainer.RegisterServices(services);
            DependencyContainer.RegisterRepository(services);
            DependencyContainer.RegisterLogger(services);
            RegisterPayments(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                GameStoreContext context = serviceScope.ServiceProvider.GetRequiredService<GameStoreContext>();
                context.Database.EnsureCreated();
            }

            loggerFactory.AddLog4Net();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}");
            });
        }

        public void RegisterPayments(IServiceCollection services)
        {
            services.AddScoped<IPaymentContext, PaymentContext>();

            services.AddScoped<IPaymentStrategy, BankPaymentStrategy>();

            services.AddScoped<IPaymentStrategy, BoxPaymentStrategy>();

            services.AddScoped<IPaymentStrategy, VisaPaymentStrategy>();
        }

        public void SetResourceNames()
        {
            ResourceNames.Urls.ForBoxPayment = Configuration.GetSection(ResourceNames.PathResourceJson.UrlsForBoxPaymant).Get<string[]>();
        }
    }
}
