using AppDomain.Contexts;
using AppDomain.Models.Interfaces;
using AppDomain.Repositories;
using AppServices.Interfaces;
using AppServices.Services;
using AppServices.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LtsParkingAppApi.App_Start
{
    public class RegisterDependancyInjections
    {

        private IServiceCollection _services;
        private IConfigurationRoot _config;


        public RegisterDependancyInjections(IServiceCollection services, IConfigurationRoot config)
        {
            _services = services;
            _config = config;
        }

        internal void RegisterAppServices()
        {
            _services.AddScoped<IUserProfileServices, UserProfileServices>();
            _services.AddScoped<IRepositoryGet, EFRepositoryGet<AppDbContext>>();
            _services.AddScoped<IRepository, EFRepository<AppDbContext>>();
            _services.AddScoped<IParkingSlotServices, ParkingSlotServices>();
            _services.AddScoped<IParkingTrafficServices, ParkingTrafficServices>();
            _services.AddScoped<IParkingAreaService, ParkingAreaService>();
        }

        internal void RegisterGenericMiddleware()
        {
            //_services.AddScoped<IRepositoryGet, EFRepositoryGet<AppDbContext>>();
            //_services.AddScoped<IRepository, EFRepository<AppDbContext>>();
        }
        }
}
