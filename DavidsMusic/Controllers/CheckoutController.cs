using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DavidsMusic.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DavidsMusic.Controllers
{
	public class CheckoutController : Controller
	{
		// GET: /<controller>/
		public IActionResult Index()
		{
			ViewData["States"] = new string[] { "Alaska","Alabama","Arkansas","American Samoa", "Arizona", "California", "Colorado", "Connecticut",
					"District of Columbia", "Delaware", "Florida", "Georgia", "Guam", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky",
					"Louisiana", "Massachusetts", "Maryland","Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina",
					"North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York","Ohio", "Oklahoma", "Oregon", "Pennsylvania",
					"Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Virgin Islands", "Vermont",
					"Washington","Wisconsin", "West Virginia", "Wyoming"
 };
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Index(CheckoutModel model)
		{
			string custFirstName = model.CustomerFirstName;

			//return this.Json(model);
			if (ModelState.IsValid)
			{
				return this.RedirectToAction("Index", "Home");
			}
			else
			{
				ViewData["States"] = new string[] { "Alaska" };
				return View();
			}

			//return View();
		}
	}
}
