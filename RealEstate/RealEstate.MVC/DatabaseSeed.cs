using RealEstate.Core.Entities;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.MVC
{
    public class DatabaseSeed
    {
        public static void SeedDatabase(RealEstateDbContext dbContext)
        {
            try
            {
                AddData(dbContext);
            }
            catch (Exception ex)
            {

            }
        }
        private static void AddData(RealEstateDbContext dbContext)
        {

            List<PropertyType> propertyTypes = new List<PropertyType>()
            {
                new PropertyType(){ Id = 1, Name = "Land"},
                new PropertyType(){ Id = 2, Name = "House"},
            };
            List<PropertyState> propertyStates = new List<PropertyState>()
            {
                new PropertyState(){Id = 1, Name = "Sold"},
                new PropertyState(){Id = 2, Name = "Active"},
                new PropertyState(){Id = 3, Name = "Inactive"},


            };
            List<PropertyStatus> propertyStatuses = new List<PropertyStatus>()
            {
                new PropertyStatus(){Id = 1, Name = "For Sale"},
                new PropertyStatus(){Id = 2, Name = "For Rent"}

            };

            List<Region> regions = new List<Region>()
            {
                new Region(){
                    Id = 1 , Name = "Banjul",
                    Cities = new List<City>()
                    {
                        new City() { Name = "Banjul"}
                    }
                },
                new Region()
                {
                    Id = 2, Name = "KMC",
                    Cities = new List<City>()
                    {
                        new City() {Name ="Kanifing"},
                       new City(){Name = "Bakau"},
                       new City(){Name = "Serekunda"},
                       new City(){Name = "Kololi"}

                    }
                },
                new Region()
                {
                    Id = 3, Name = "West Coast Region",
                    Cities = new List<City>()
                    {
                        new City() {Name ="Brikama"},
                       new City(){Name = "Tanji"},
                       new City(){Name = "Gunjur"},
                       new City(){Name = "Sanyang"},
                       new City(){Name = "Bwian"}


                    }
                },
                new Region()
                {
                    Id = 4, Name = "Lower River Region",
                    Cities = new List<City>()
                    {
                        new City() {Name ="Soma"},
                       new City(){Name = "Mansa Konko"},
                       new City(){Name = "Tendaba "},



                    }
                },
                  new Region()
                {
                    Id = 5, Name = "Upper River Region",
                    Cities = new List<City>()
                    {
                        new City() {Name ="Basse"},



                    }
                },
                     new Region()
                {
                    Id = 6, Name = "North Bank Region",
                    Cities = new List<City>()
                    {
                        new City() {Name ="Janjanbureh"},



                    }
                },
            };


            if (!dbContext.PropertyStatus.Any()) dbContext.PropertyStatus.AddRange(propertyStatuses);
            if (!dbContext.PropertyState.Any()) dbContext.PropertyState.AddRange(propertyStates);
            if (!dbContext.PropertyType.Any()) dbContext.PropertyType.AddRange(propertyTypes);
            if (!dbContext.Region.Any()) dbContext.Region.AddRange(regions);

            dbContext.SaveChanges();


            
        }
    }
}
