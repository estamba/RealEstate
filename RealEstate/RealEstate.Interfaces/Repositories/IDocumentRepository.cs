using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Repositories
{
    public interface IDocumentRepository : IBaseRepository<Document>
    {
        Document GetDocumentByImageId(Guid imageId);
    }
}
