using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealEstate.Repositories
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository 
    {
        private readonly RealEstateDbContext dbContext;

        public DocumentRepository(RealEstateDbContext dbContext): base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Document GetDocumentByImageId(Guid imageId)
        {
            return dbContext.Image.Where(i => i.Id == imageId).Select(i => i.Document).FirstOrDefault();
        }
    }
}
