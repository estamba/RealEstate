using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
    public class EmailModel
    {
        public string HtmlContent { get; set; }
        public string PlainContent { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
    }
}
