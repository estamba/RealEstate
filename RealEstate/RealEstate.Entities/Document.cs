using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Entities
{
  public  class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

    }
}
