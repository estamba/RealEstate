using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
    
    public class UpdateProfileModel
    {
        public Guid AgentId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }

        public Document ProfilePhoto { get; set; }

    }
}
