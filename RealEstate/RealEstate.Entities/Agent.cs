using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
    public class Agent
    {
        public Guid Id { get; set; }
        public Image Image { get; set; }
        public Guid? ImageId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public bool Isverified { get; set; }
        public City City { get; set; }
        public int? CityId { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public ICollection<Property> Properties { get; set; }


    }
}
