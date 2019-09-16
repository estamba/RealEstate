using Microsoft.AspNetCore.Identity;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Services.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstate.MVC.Services
{
    public class DashboardVmService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAgentService agentService;

        public DashboardVmService(UserManager<ApplicationUser> userManager, IAgentService agentService)
        {
            this.userManager = userManager;
            this.agentService = agentService;
        }
        public async Task<Guid> GetUserImageId(ClaimsPrincipal principal )
        {
            string emailAddress = principal.Identity.Name;
            var user = await userManager.FindByEmailAsync(emailAddress);
            var agent = await agentService.GetAgentByApplicationUserIdAsync(user.Id);
            return (agent.ImageId is null) ? default : agent.ImageId.Value;
        }
    }
}
