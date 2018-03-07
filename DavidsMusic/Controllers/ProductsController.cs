using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DavidsMusic.Models;
using Microsoft.EntityFrameworkCore;

namespace DavidsMusic.Controllers
{		
	public class ProductsController : Controller
	{
		private DavidTestContext _context;

	//	private Models.ConnectionStrings _connectionStrings;
		public ProductsController(DavidTestContext context)
		{
			_context = context;
			//_connectionStrings = connectionStrings.Value;
		}

		public IActionResult Index(int? ID=1)
		{
		//	Models.ProductsViewModel model = new Models.ProductsViewModel();
		//	System.Data.Common.DbConnectionStringBuilder builder =
		//		new System.Data.Common.DbConnectionStringBuilder();

			//var product = _context.Products.Find(ID);
			//var product = _context.Products.Include(x => x.Reviews).Single(x => x.Id == ID);
			var product = _context.Products.AsNoTracking().Include(x => x.Reviews).Single(x => x.Id == ID); 
																											 
			return View(product);
		}

		[HttpPost]
		public IActionResult Index(int id)
		{
			Guid cartId;
			Cart c;
			CartItem i;
			if (Request.Cookies.ContainsKey("cartId") && Guid.TryParse(Request.Cookies["cartId"], out cartId) && _context.Cart.Any(x => x.TrackingNumber == cartId))
			{
				c = _context.Cart
					.Include(x => x.CartItems)
					.ThenInclude(y => y.Product)
					.Single(x => x.TrackingNumber == cartId);
			}
			else
			{
				c = new Cart();
				cartId = Guid.NewGuid();
				c.TrackingNumber = cartId;
				_context.Cart.Add(c);
			}

			if (User.Identity.IsAuthenticated)
			{
				c.user = _context.Users.Find(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
			}

			if (c.CartItems.Any(x => x.Product.Id == id))
			{
				i = c.CartItems.FirstOrDefault(x => x.Product.Id == id);
			}
			else
			{
				i = new CartItem();
				i.Cart = c;
				i.Product = _context.Products.Find(id);
				c.CartItems.Add(i);
			}
			i.Quantity++;

			_context.SaveChanges();
			Response.Cookies.Append("cartId", c.TrackingNumber.ToString(),
				new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTime.Now.AddYears(1)
				});
	

//				if (!Request.Cookies.ContainsKey("cartId"))
//				{
//					var product = _context.Products.Find(id);
//					Order o = new Order();
//					LineItem l = new LineItem();
//					l.Quantity = 1;
//					l.Product = product;
//					o.LineItems.Add(l);
//					o.SubmittedDate = DateTime.UtcNow;
//					o.SubTotal = product.UnitPrice;
//					o.TasTotal = o.SubTotal * 0.1m;
//					o.ShippingTotal = 10m;
//					o.TrackingNumber = Guid.NewGuid();
//			
//					_context.Orders.Add(o);
//					_context.SaveChanges();
//					cartId = o.TrackingNumber.ToString();
//			
//					//Cookies: useful for saving small pieces of non-sensitive data for a long period of time
//					Response.Cookies.Append("cartId", cartId, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddYears(1) });
//				}
//				else
//				{
//					Request.Cookies.TryGetValue("cartId", out cartId);
//				}
//				Console.WriteLine("Added {0} to cart {1}", id, cartId);
//				//TODO: At this point, I'd create a record in the database that
//				//corresponds to this cart ID, and add the product to that cart
//			
//				byte[] objectBytes = null;
//				BinaryFormatter bf = new BinaryFormatter();
//				using (MemoryStream ms = new MemoryStream())
//				{
//					bf.Serialize(ms, id);
//					objectBytes = ms.ToArray();
//				}
//				HttpContext.Session.Set(cartId, objectBytes);
//

			return RedirectToAction("Index", "Checkout");
		}
	}
}
