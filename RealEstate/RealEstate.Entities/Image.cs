using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
   public class Image
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }
        public ICollection<PropertyImage> PropertyImages { get; set; }

    }
}
