using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Interfaces.Services.Agents
{
  public  interface IAgentService
    {
        Task<Agent> GetAgentByApplicationUserIdAsync(string applicationUserId);
    }
}
