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
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository agentRepository;

        public AgentService(IAgentRepository agentRepository)
        {
            this.agentRepository = agentRepository;
        }
        public async Task<Agent> GetAgentByApplicationUserIdAsync(string applicationUserId)
        {
            return ( await agentRepository.FindAsync(a => a.ApplicationUserId == applicationUserId)).FirstOrDefault();
        }
    }
}
