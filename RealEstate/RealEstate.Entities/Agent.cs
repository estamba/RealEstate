using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
    public class Agent
    {
        public Guid Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public bool Isverified { get; set; }

        public ICollection<Property> Properties { get; set; }


    }
}
