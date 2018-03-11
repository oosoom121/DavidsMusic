using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidsMusic.Models
{
    public class ProductsViewModel
    {
		public ProductsViewModel()
		{
			Reviews = new HashSet<Review>();
			CartItems = new HashSet<CartItem>();
			LineItems = new HashSet<LineItem>();
		}

		public int ID { get; set; }
		public int StockNumber { get; set; }
		public string Type { get; set; }
		public string Brand { get; set; }
		public string Description { get; set; }
		public decimal UnitPrice { get; set; }
		public string ImageUrl { get; set; }

		public Categories Category { get; set; }
		public ICollection<Review> Reviews { get; set; }
		public ICollection<LineItem> LineItems { get; set; }


		public ICollection<CartItem> CartItems { get; set; }
	}
}
