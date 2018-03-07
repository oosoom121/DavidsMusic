using System;
using System.Collections.Generic;

namespace DavidsMusic.Models
{
    public class Cart
    {
		public Cart()
		{
			CartItems = new HashSet<CartItem>();
		}
		public int ID { get; set; }
		public Guid TrackingNumber { get; set; }
		public ApplicationUser user { get; set; }
		public ICollection<CartItem> CartItems { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateLastModified { get; set; }
	}
}
