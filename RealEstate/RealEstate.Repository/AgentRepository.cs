using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Repositories
{
    public class AgentRepository : BaseRepository<Agent>, IAgentRepository
    {
        public AgentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
