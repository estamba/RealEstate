using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.Repositories;
using RealEstate.Core.Interfaces.Services.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Services.Documents
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }
        public Document GetDocumentByImageId(Guid imageId)
        {
            return documentRepository.GetDocumentByImageId(imageId);

        }
    }
}
