using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidsMusic.Models
{
    public class CartItem
    {
		public int ID { get; set; }
		public int Quantity { get; set; }
		public Products Products { get; set; }
		public Cart Cart { get; set; }
	}
}
