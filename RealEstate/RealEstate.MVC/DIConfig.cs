using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Agents;
using RealEstate.Core.Interfaces.Services.Metadata;
using RealEstate.Core.Interfaces.Services.Properties;
using RealEstate.Core.Interfaces.Services.Regions;
using RealEstate.Core.Services.Agents;
using RealEstate.Core.Services.Metadata;
using RealEstate.Core.Services.Properties;
using RealEstate.Core.Services.Regions;
using RealEstate.Repositories;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.MVC
{
    public static class DIConfig
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAddAgentService, AddAgentService>();
            services.AddTransient<IAgentService, AgentService>();
            services.AddTransient<IMetadataService, MetadataService>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<IAddPropertyService, AddPropertyService>();




            return services;
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddTransient<IAgentRepository, AgentRepository>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IRegionRepository, RegionRepository>();

            services.AddScoped<DbContext, RealEstateDbContext>();
        }
    }
}
