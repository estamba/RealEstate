using AutoMapper;
using RealEstate.Core.Entities;
using RealEstate.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.MVC
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {

            CreateMap<AddPropertyVm, PostPropertyModel>().ReverseMap();

        }
    }
}
