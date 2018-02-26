using System;
using System.Collections.Generic;

namespace DavidsMusic.Models
{
    public partial class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
    }
}
