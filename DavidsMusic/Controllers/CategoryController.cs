using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DavidsMusic.Models;

namespace DavidsMusic.Controllers
{
    public class CategoryController : Controller
    {
		private DavidTestContext _context;

		public CategoryController(DavidTestContext context)
		{
			_context = context;
		}

		public IActionResult Index(int id)
		{
			var category = _context.Categories.Include(x => x.Products)
				.SingleOrDefault(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}
	
			return View(category);
		}
	}
}
