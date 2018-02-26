using System;
using System.Collections.Generic;

namespace DavidsMusic.Models
{
    public partial class ProductsCategories
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public Categories Category { get; set; }
        public Products Product { get; set; }
    }
}
