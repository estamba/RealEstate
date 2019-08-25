using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Interfaces.Services.Documents
{
    public interface IDocumentService 
    {

        Document GetDocumentByImageId(Guid imageId);
    }
}
