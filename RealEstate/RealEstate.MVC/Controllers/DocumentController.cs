using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Core.Interfaces.Services.Documents;

namespace RealEstate.MVC.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService documentService;

        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }
        public IActionResult GetImage(Guid ImageId)
        {

            if (ImageId == Guid.Empty) return NotFound();
            var document = documentService.GetDocumentByImageId(ImageId);

            return File(document.Content, document.ContentType);

        }
    }
}