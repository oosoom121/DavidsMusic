using System;
using System.Collections.Generic;

namespace DavidsMusic.Models
{
    public partial class Cart
    {
        public Guid CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }

        public Products Product { get; set; }
    }
}
