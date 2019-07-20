using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Agents;
using RealEstate.Core.Services.Agents;
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
            return services;
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddTransient<IAgentRepository, AgentRepository>();
            services.AddScoped<DbContext, RealEstateDbContext>();
        }
    }
}
