using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Services.Properties
{
    public class AddPropertyService : IAddPropertyService
    {
        private readonly IPropertyRepository propertyRepository;

        public AddPropertyService(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }
        public void Add(PostPropertyModel postPropertyModel)
        {
           
            Property property = new Property()
            {
                AgentId = postPropertyModel.AgentId,
                Area = postPropertyModel.Area,
                DateCreated = DateTime.Now,
                Description = postPropertyModel.Description,
                Price = (decimal)postPropertyModel.Price,
                Title = postPropertyModel.Title,
                CityId =  postPropertyModel.SelectedCity.Value,
                StatusId = (short)postPropertyModel.SelectedStatus,
                TypeId =  (short)postPropertyModel.SelectedType,
                PropertyImages =  GetPropertyImages(postPropertyModel.Documents),
             

            };

            propertyRepository.Add(property);
        }
        private List<PropertyImage> GetPropertyImages(List<Document> documents)
        {
            List<PropertyImage> propertyImages = new List<PropertyImage>();
            foreach (var document in documents)
            {
                PropertyImage propertyImage = new PropertyImage()
                {
                    Image = new Image() { Id = Guid.NewGuid(), Document = document },

                };
                propertyImages.Add(propertyImage);
            }
            return propertyImages;
        }
    }
}
