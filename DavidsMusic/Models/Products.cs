using System;
using System.Collections.Generic;

namespace DavidsMusic.Models
{
    public partial class Products
    {
        public Products()
        {
			Reviews = new HashSet<Review>();
            Cart = new HashSet<Cart>();
            ProductsCategories = new HashSet<ProductsCategories>();
			LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public int? StockNumber { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }

		public Categories Category { get; set; }
		public ICollection<Review> Reviews { get; set; }
		public ICollection<LineItem> LineItems { get; set; }


		public ICollection<Cart> Cart { get; set; }
        public ICollection<ProductsCategories> ProductsCategories { get; set; }

    }
}
