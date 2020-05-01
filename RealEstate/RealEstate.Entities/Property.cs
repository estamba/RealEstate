using System;
using System.Collections;
using System.Collections.Generic;

namespace RealEstate.Core.Entities
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PropertyType Type { get; set; }
        public short TypeId { get; set; }

        public Agent Ágent { get; set; }
        public Guid AgentId { get; set; }
        public double Area { get; set; }
        public PropertyStatus Status { get; set; }
        public short StatusId  { get; set; }
        public int ViewCount { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }

        public short StateId { get; set; }
        public PropertyState State { get; set; }
        public bool IsDelete { get; set; }


        public ICollection<PropertyImage> PropertyImages { get; set; }

    }
}
