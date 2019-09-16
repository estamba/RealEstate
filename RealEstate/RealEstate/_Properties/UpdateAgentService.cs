using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Agents;
using RealEstate.Core.Interfaces.Services.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Services.Properties
{
    public class UpdateAgentService : IUpdateAgentService
    {
        private readonly IAgentRepository agentRepository;
        private readonly ILocationService locationService;

        public UpdateAgentService(IAgentRepository agentRepository, ILocationService locationService)
        {
            this.agentRepository = agentRepository;
            this.locationService = locationService;
        }
        public async Task<Agent> UpdateProfile(UpdateProfileModel updateProfileModel)
        {
            var agent = (await agentRepository.FindAsync(a => a.Id == updateProfileModel.AgentId, "ApplicationUser")).FirstOrDefault();
            agent.CityId = GetCityId(updateProfileModel.City);
            agent.ApplicationUser.Name = updateProfileModel.Name;
            agent.Image = agent.Image ?? GetImage(updateProfileModel.ProfilePhoto);

            return agentRepository.Update(agent);
        }
        private int? GetCityId(string cityName)
        {
            if (string.IsNullOrEmpty(cityName)) return default;
            return locationService.GetCities().Where(c => c.Name.ToLower() == cityName.ToLower()).Select(c => c.Id).FirstOrDefault();
        }
        private Image GetImage(Document document)
        {
            if (document is null) return default;

            return new Image()
            {
                Id = Guid.NewGuid(),
                Document = document
            };

        }
    }
}
