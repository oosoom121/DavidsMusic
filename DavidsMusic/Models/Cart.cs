using System;
using System.Collections.Generic;

namespace DavidsMusic.Models
{
    public partial class Cart
    {
		public Cart()
		{
			CartItems = new HashSet<CartItem>();
		}
		public Guid CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
		public Guid TrackingNumber { get; set; }
		public ApplicationUser user { get; set; }

		public Products Product { get; set; }
		public ICollection<CartItem> CartItems { get; set; }
	}
}
