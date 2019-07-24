﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }
        public int RegionId { get; set; }

    }
}
