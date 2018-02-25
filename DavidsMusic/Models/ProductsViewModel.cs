using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidsMusic.Models
{
    public class ProductsViewModel
    {
		public int ID { get; set; }
		public int StockNumber { get; set; }
		public string Type { get; set; }
		public string Brand { get; set; }
		public string Description { get; set; }
		public decimal UnitPrice { get; set; }
		public string ImageUrl { get; set; }

	}
}
