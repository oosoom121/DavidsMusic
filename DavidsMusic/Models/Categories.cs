using System;
using System.Collections.Generic;

namespace DavidsMusic.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
