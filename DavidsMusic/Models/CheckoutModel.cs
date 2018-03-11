using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DavidsMusic.Models
{
    public class CheckoutModel
    {
		public CartItem[] CartItems { get; set; }

		[Required]
		[Display(Name="First Name")]
		public string CustomerFirstName { get; set; }
		[Display(Name = "Last Name")]
		public string CustomerLastName { get; set; }
		[Display(Name = "Address 1")]
		public string CustomerAddress1 { get; set; }
		[Display(Name = "Address 2")]
		public string CustomerAddress2 { get; set; }
		[Required]
		[Display(Name = "City")]
		public string CustomerCity { get; set; }
		[Display(Name = "State")]
		public string State { get; set; }
		[Display(Name = "Postal Code")]
		public string CustomerPostal { get; set; }
		public string CostomerHomePhone { get; set; }
		public string CustomerCellPhone { get; set; }
		[Required]
		[EmailAddress]
		public string CustomerEmail { get; set; }
		public string CustomerShippingAddress1 { get; set; }
		public string CustomerShippingAddress2 { get; set; }
		public string CustomerShippingCity { get; set; }
		public string CustomerShippingState { get; set; }
		public string CustomerShippingPostalCode { get; set; }
		public string CustomerBillingAddress1 { get; set; }
		public string CustomerBillingAddress2 { get; set; }
		public string CustomerBillingCity { get; set; }
		public string CustomerBillingState { get; set; }
		public string CustomerBillingPostalCode { get; set; }

		[Required]
		public string creditcardnumber { get; set; }
		[Required]
		public string creditcardname { get; set; }
		[Required]
		public string creditcardverificationvalue { get; set; }
		[Required]
		public string expirationmonth { get; set; }
		[Required]
		public string expirationyear { get; set; }

	}
}
