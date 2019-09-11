using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Property Add(PostPropertyModel postPropertyModel)
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
                StateId = 2// Active
               
             

            };

            return propertyRepository.Add(property);
        }

        public void Delete(Guid Id)
        {
            var property = propertyRepository.GetPropertyById(Id);
            property.IsDelete = true;

            propertyRepository.Update(property);
        }

        public void Update(EditPropertyModel editPropertyModel)
        {
            var property = propertyRepository.GetPropertyById(editPropertyModel.Id);


            property.Area = editPropertyModel.Area;
            property.CityId = (short)editPropertyModel.SelectedCity;
            property.Description = editPropertyModel.Description;
            property.Price = (decimal)editPropertyModel.Price;
            property.Title = editPropertyModel.Title;
            property.TypeId = (short)editPropertyModel.SelectedType;
            property.StatusId = (short)editPropertyModel.SelectedStatus;



            propertyRepository.Update(property);
        }

        public void UpdatePropertyState(Guid Id, string state)
        {

            var property = propertyRepository.GetPropertyById(Id);
            var propertyStates = propertyRepository.GetPropertyStates();
            property.StateId = (short)propertyStates.Where(s=>s.Name?.ToLower() == state.ToLower()).FirstOrDefault()?.Id;
            propertyRepository.Update(property);
            
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
