using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Agents;

namespace RealEstate.Core.Services.Agents
{
    public class AddAgentService : IAddAgentService
    {
        private readonly IAgentRepository agentRepository;

        public AddAgentService(IAgentRepository agentRepository)
        {
            this.agentRepository = agentRepository;
        }
        public Agent Add(Agent agent)
        {

            agentRepository.Add(agent);
            return agent;
        }
    }
}
