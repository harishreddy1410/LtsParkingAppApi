using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Sinks.MSSqlServer;

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
            _mapperConfiguration = new AutoMapper.MapperConfiguration(cfg =>
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    );
            });

            ConfigureSerilog(services);
            services.AddScoped<IUserProfileServices, UserProfileServices>();
            services.AddScoped<IRepositoryGet, EFRepositoryGet<AppDbContext>>();
            services.AddScoped<IRepository, EFRepository<AppDbContext>>();
            services.AddScoped<IParkingSlotServices, ParkingSlotServices>();
            services.AddScoped<IParkingTrafficServices, ParkingTrafficServices>();
            services.AddScoped<IGenericServices, GenericServices>();
            services.AddSingleton<AutoMapper.IMapper>(sp => _mapperConfiguration.CreateMapper());
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var registerDI = new RegisterDependancyInjections(services, _config);
           
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["AppDbContext"]));
            services
                .AddMvc()
                .AddMvcOptions(options => {
                    options.Filters.Add(typeof(ApiRequestValidator));
                    options.Filters.Add(typeof(GlobalExceptionFilter));
                        }
                )
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            loggerFactory.AddConsole(_config.GetSection("Serilog"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }

        private void ConfigureSerilog(IServiceCollection services)
        {
            //Documentation  - https://github.com/serilog/serilog-sinks-mssqlserver
            
            services.AddSingleton<Serilog.ILogger>(x =>
            {
                return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.MSSqlServer(_config["ConnectionStrings:AppDbContext"], "Logs", autoCreateSqlTable: false)
                .Enrich.FromLogContext()
                .CreateLogger();
            });

            Serilog.Debugging.SelfLog.Enable(Console.Out);
        }
    }
}
