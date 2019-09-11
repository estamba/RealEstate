using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Services.Agents
{
    public class AgentPropertyStatsService : IAgentPropertyStatsService
    {
        private readonly IPropertyRepository propertyRepository;

        public AgentPropertyStatsService(IPropertyRepository propertyRepository )
        {
            this.propertyRepository = propertyRepository;
        }
        public async Task<AgentPropertyStats> Get(Guid agentId)
        {
            var properties = await propertyRepository.GetPropertiesByAgentIDAsync(agentId, PropertySortOptions.Default);
            AgentPropertyStats stats = new AgentPropertyStats()
            {
                ActivePropertyCount = properties.Where(p => p.State.Name?.ToLower() == "active").Count(),
                ViewCount = properties.Sum(p=>p.ViewCount),
                PropertyCount = properties.Where(p=>!p.IsDelete).Count(),
                SoldPropertyCount = properties.Where(p=>p.State.Name?.ToLower() == "sold").Count()
              
            };
            return stats;
        }
    }
}
