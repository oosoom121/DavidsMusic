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

			//string connectionString = ;
			//			using (var connection = new System.Data.SqlClient.SqlConnection(_connectionStrings.DefaultConnection))
			//
			//			{
			//				connection.Open();
			//
			//				var command = connection.CreateCommand();
			//				command.CommandText = "sp_GetProduct";
			//				command.Parameters.AddWithValue("@id", ID);
			//				command.CommandType = System.Data.CommandType.StoredProcedure;
			//
			//				//command.CommandText = "SELECT * FROM Products WHERE ID = " + ID.Value;
			//				using (var reader = command.ExecuteReader())
			//				{
			//					var stocknumCol = reader.GetOrdinal("StockNumber");
			//					var typeCol = reader.GetOrdinal("Type");
			//					var BrandCol = reader.GetOrdinal("Brand");
			//					var descrCol = reader.GetOrdinal("Description");
			//					var priceCol = reader.GetOrdinal("UnitPrice");
			//					var imageUrlCol = reader.GetOrdinal("ImageUrl"); 
			//
			//					while (reader.Read())
			//					{
			//						model.StockNumber = reader.IsDBNull(stocknumCol) ? 0 : reader.GetInt32(stocknumCol);
			//						model.Type = reader.IsDBNull(typeCol) ? "" : reader.GetString(typeCol);
			//						model.Brand = reader.IsDBNull(BrandCol) ? "" : reader.GetString(BrandCol);
			//						model.Description = reader.IsDBNull(descrCol) ? "" : reader.GetString(descrCol);
			//						model.UnitPrice = reader.IsDBNull(priceCol) ? 0 : reader.GetDecimal(priceCol);
			//						model.ImageUrl = reader.IsDBNull(imageUrlCol) ? "" : reader.GetString(imageUrlCol);
			//					}
			//				}
			//				connection.Close();
			//			}

			var product = _context.Products.Find(ID);
			return View(product);
		}

		//	[HttpPost]
		//	public IActionResult Index(string num)
		//	{
		//		// Cookies: Useful for saving small pieces of non-sensitive data for a long period of time.
		//		Response.Cookies.Append("productStockNumber", num);
		//		return RedirectToAction("Index", "Checkout");
		//	}
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
