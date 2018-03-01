using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DavidsMusic.Models;

namespace DavidsMusic.Controllers
{
    public class HomeController : Controller
    {

		private DavidTestContext _context;

		//	private Models.ConnectionStrings _connectionStrings;
		public HomeController(DavidTestContext context)
		{
			_context = context;
			//_connectionStrings = connectionStrings.Value;
		}
		public IActionResult Index()
        {
			//return View(_context.Products.Take(4));
			return View(_context.Products);
		}

		public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

		public IActionResult Register()
		{
			return View();
		}

		public IActionResult Profile()
		{
			return View();
		}

		public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
