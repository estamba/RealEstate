using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
    public class PropertyImage
    {
        public Property Property { get; set; }
        public Guid PropertyId { get; set; }

        public Image  Image { get; set; }
        public Guid ImageId { get; set; }

    }
}
