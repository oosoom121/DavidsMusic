using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DavidsMusic.Models;

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

		// GET: /<controller>/
		[HttpGet]
		public IActionResult Index(int? ID)
		{
			Models.ProductsViewModel model = new Models.ProductsViewModel();
			System.Data.Common.DbConnectionStringBuilder builder =
				new System.Data.Common.DbConnectionStringBuilder();

			var product = _context.Products.Find(ID);
			return View(product);
		}

		[HttpPost]
		public IActionResult Index(string id)
		{
			string cartId;
			if (!Request.Cookies.ContainsKey("cartId"))
			{
				cartId = Guid.NewGuid().ToString();

				//Cookies: useful for saving small pieces of non-sensitive data for a long period of time
				Response.Cookies.Append("cartId", cartId, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddYears(1) });
			}
			else
			{
				Request.Cookies.TryGetValue("cartId", out cartId);
			}
			Console.WriteLine("Added {0} to cart {1}", id, cartId);
			//TODO: At this point, I'd create a record in the database that
			//corresponds to this cart ID, and add the product to that cart

			byte[] objectBytes = null;
			BinaryFormatter bf = new BinaryFormatter();
			using (MemoryStream ms = new MemoryStream())
			{
				bf.Serialize(ms, id);
				objectBytes = ms.ToArray();
			}
			HttpContext.Session.Set(cartId, objectBytes);


			return RedirectToAction("Index", "Checkout");
		}
	}
}
