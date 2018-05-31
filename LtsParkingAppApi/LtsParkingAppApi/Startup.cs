using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomain.Contexts;
using AppDomain.Models.Interfaces;
using AppDomain.Repositories;
using AppServices.AutoMapperProfileConfiguration;
using AppServices.Interfaces;
using AppServices.Services;
using AppServices.UserService;
using LtsParkingAppApi.App_Start;
using LtsParkingAppApi.Helpers.AutoMapperProfile;
using LtsParkingAppApi.Helpers.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LtsParkingAppApi
{
    public class Startup
    {
        private IConfigurationRoot _config;
        private IHostingEnvironment _env;
        private AutoMapper.MapperConfiguration _mapperConfiguration { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            _mapperConfiguration = new  AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
                cfg.AddProfile(new AutoMapperProfileViewModelConfiguration());
            });

            _config = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserProfileServices, UserProfileServices>();

            services.AddScoped<IRepositoryGet, EFRepositoryGet<AppDbContext>>();
            services.AddScoped<IRepository, EFRepository<AppDbContext>>();
            services.AddScoped<IParkingSlotServices, ParkingSlotServices>();
            services.AddScoped<IParkingTrafficServices, ParkingTrafficServices>();
            services.AddSingleton<AutoMapper.IMapper>(sp => _mapperConfiguration.CreateMapper());


            var registerDI = new RegisterDependancyInjections(services, _config);
            registerDI.RegisterGenericMiddleware();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["AppDbContext"]));           
            services
                .AddMvc()
                .AddMvcOptions(options => options.Filters.Add(typeof(ApiRequestValidator)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
    }
}
