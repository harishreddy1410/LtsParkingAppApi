using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomain.Contexts;
using AppDomain.Models.Interfaces;
using AppDomain.Repositories;
using AppServices.UserService;
using LtsParkingAppApi.App_Start;
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

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserProfileServices, UserProfileServices>();

            services.AddScoped<IRepositoryGet, EFRepositoryGet<AppDbContext>>();
            services.AddScoped<IRepository, EFRepository<AppDbContext>>();



            var registerDI = new RegisterDependancyInjections(services, _config);
            registerDI.RegisterGenericMiddleware();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["AppDbContext"]));
            services.AddMvc();
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
