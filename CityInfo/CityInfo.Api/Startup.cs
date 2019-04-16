using CityInfo.Api.Entities;
using CityInfo.Api.Services;
using CityInfo.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CityInfo.Api
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add 
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddMvcOptions(x => x.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));

            services.AddDbContext<CityInfoContext>(options =>
                   options.UseSqlServer(Configuration["ConnectionString:CityDb"]));

            services.AddScoped<ICityInfoRepository, CityInfoRepository>();

#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();

#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            
            // Option for name strategy for json response, we will get data in the same LatterCase as our model. 

            //   .AddJsonOptions(o =>
            //{
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver
            //            as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CityInfoContext cityInfoContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            else
            {
                // app.UseExceptionHandler();
            }

            cityInfoContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();
            app.UseMvc();

            AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
                    cfg.CreateMap<Entities.City, Models.CityDto>();
                    cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
                    cfg.CreateMap<Models.PointOfInterestCreationDto, Entities.PointOfInterest>();
                    cfg.CreateMap<Models.PointOfInterestUpdateDto, Entities.PointOfInterest>();
                    cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestUpdateDto>();


                });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
