using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DavidsMusic.Models;

namespace DavidsMusic.Controllers
{
    public class AccountController : Controller
    {
		private SignInManager<ApplicationUser> _signInManager;

		public AccountController(SignInManager<ApplicationUser> signInManager)
		{
			this._signInManager = signInManager;
		}

		// GET: /<controller>/
		[Microsoft.AspNetCore.Authorization.Authorize]
		public IActionResult Index()
        {
			return Content("You can only see this if you're logged in");
        }

//  *********  LOGIN
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Login(string username, string password)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser existingUser = _signInManager.UserManager.FindByNameAsync(username).Result; // Identify user by username
				if (existingUser != null)
				{
					// If username exists, validate password
					if (_signInManager.UserManager.CheckPasswordAsync(existingUser, password).Result)
					{
						_signInManager.SignInAsync(existingUser, false).Wait();
						return RedirectToAction("Index", "Home");
					}
					else
					{
						ModelState.AddModelError("username", "Username or password is incorrect.");
					}

				}
				else
				{
					ModelState.AddModelError("username", "Username or password is incorrect.");
				}

			}
			else
			{
			}
			return View();
		}
    }
}
