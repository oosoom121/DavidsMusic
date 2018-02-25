using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DavidsMusic.Controllers
{
    public class AccountController : Controller
    {
		private SignInManager<IdentityUser> _signInManager;

		public AccountController(SignInManager<IdentityUser> signInManager)
		{
			this._signInManager = signInManager;
		}

		// GET: /<controller>/
		[Microsoft.AspNetCore.Authorization.Authorize]
		public IActionResult Index()
        {
			return Content("You can only see this if you're logged in");
        }
/*
 * // *******  REGISTER
		public IActionResult Register()
		{
			return View();
		}

		public IActionResult Logout()
		{		
			_signInManager.SignOutAsync().Wait();
			return RedirectToAction("Index", "Home");
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Register(string username, string password)
		{
			if (ModelState.IsValid)
			{
				//Do something with SignInManager here
				IdentityUser newUser = new IdentityUser(username);
				
				var userResult = _signInManager.UserManager.CreateAsync(newUser).Result;
				if (userResult.Succeeded)
				{
					var passwordResult = _signInManager.UserManager.AddPasswordAsync(newUser, password).Result;
					if (passwordResult.Succeeded)
					{
						_signInManager.SignInAsync(newUser, false).Wait();
						return RedirectToAction("Index", "Home");
					}
					else
					{
						foreach (var error in passwordResult.Errors)
						{
							ModelState.AddModelError(error.Code, error.Description);
						}
						_signInManager.UserManager.DeleteAsync(newUser).Wait();
					}
				}
				else
				{
					foreach (var error in userResult.Errors)
					{
						ModelState.AddModelError(error.Code, error.Description);
					}
					_signInManager.UserManager.DeleteAsync(newUser).Wait();
				}
			}
			return View();
		}
*/
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
				IdentityUser existingUser = _signInManager.UserManager.FindByNameAsync(username).Result; // Identify user by username
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
