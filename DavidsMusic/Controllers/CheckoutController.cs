using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DavidsMusic.Models;
using Microsoft.EntityFrameworkCore;


namespace DavidsMusic.Controllers
{
	public class CheckoutController : Controller
	{
		private DavidTestContext _context;

		private Braintree.BraintreeGateway _braintreeGateway;

		public CheckoutController(DavidTestContext context, Braintree.BraintreeGateway braintreeGateway)
		{
			_context = context;
            _braintreeGateway = braintreeGateway;
		}

		// GET: /<controller>/
		public IActionResult Index()
		{
			string cartId;
			Guid trackingNumber;
			CheckoutModel model = new CheckoutModel();
			if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out trackingNumber) && _context.Cart.Any(x => x.TrackingNumber == trackingNumber))
			{

				var cart = _context.Cart.Include(x => x.CartItems).ThenInclude(y => y.Product).Single(x => x.TrackingNumber == trackingNumber);
				model.CartItems = cart.CartItems.ToArray();
			}
			return View(model);
		}

		[HttpPost]
		public IActionResult Update(int quantity, int productId)
		{
			string cartId;
			Guid trackingNumber;
			if (Request.Cookies.TryGetValue("cartId", out cartId) && Guid.TryParse(cartId, out trackingNumber) && _context.Cart.Any(x => x.TrackingNumber == trackingNumber))
			{

				var cart = _context.Cart.Include(x => x.CartItems).ThenInclude(y => y.Product).Single(x => x.TrackingNumber == trackingNumber);
				var cartItem = cart.CartItems.Single(x => x.Product.ID == productId);
				cartItem.Quantity = quantity;

				if (cartItem.Quantity == 0)
				{
					_context.CartItems.Remove(cartItem);
				}
				_context.SaveChanges();
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Index(CheckoutModel model)
		{
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{5}(?:[-\s]\d{4})?$");
			if (string.IsNullOrEmpty(model.CustomerPostal) || !regex.IsMatch(model.CustomerPostal))
			{
				ModelState.AddModelError("Zip", "The zip code is invalid!");
			}

			if (ModelState.IsValid)
			{
				//If model state is valid, proceed to the next step.
				return this.RedirectToAction("Index", "Home");
			}
			return View();
		}


//		[HttpPost]
//		[ValidateAntiForgeryToken]
//		public IActionResult Index(CheckoutModel model)
//		{
//			string custFirstName = model.CustomerFirstName;
//
//			//return this.Json(model);
//			if (ModelState.IsValid)
//			{
//				return this.RedirectToAction("Index", "Home");
//			}
//			else
//			{
//				ViewData["States"] = new string[] { "Alaska" };
//				return View();
//			}
//			
//			//return View();
//		}

	//	public IActionResult ValidateAddress()
	//	{
	//		SmartyStreets.USStreetApi.Lookup lookup = new SmartyStreets.USStreetApi.Lookup();
	//		lookup.Street = "222 W Ontario";
	//		lookup.City = "Chicago";
	//		lookup.State = "IL";
	//		_usSteetClient.Send(lookup);
	//		return Json(lookup.Result);
	//	}
	}
}
