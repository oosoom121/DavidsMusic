using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using DavidsMusic.Models;
using System.Threading.Tasks;
using SendGrid;
using Microsoft.AspNetCore.Http.Extensions;
using System;

namespace DavidsMusic.Controllers
{
	public class AccountController : Controller
	{
		private SignInManager<ApplicationUser> _signInManager;
		private SendGridClient _sendGridClient;

		public AccountController(SignInManager<ApplicationUser> signInManager, SendGrid.SendGridClient sendGridClient)
		{
			this._signInManager = signInManager;
			this._sendGridClient = sendGridClient;
		}

		// GET: /<controller>/
		[Microsoft.AspNetCore.Authorization.Authorize]
		public IActionResult Index()
		{
			return Content("You can only see this if you're logged in");
		}

		public IActionResult Register()
		{
			return View();
		}

		//  *********  LOGIN
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(string username, string password)
		{
			// TODO: Test user name for email address format. /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
			if (string.IsNullOrEmpty(username) || !regex.IsMatch(username))
			{
				ModelState.AddModelError("username", "The email address entered is invalid!");
			}

			if (ModelState.IsValid)
			{
				ApplicationUser existingUser = await _signInManager.UserManager.FindByNameAsync(username); // Identify user by username
				if (existingUser != null)
				{
					// If username exists, validate password
					if (await _signInManager.UserManager.CheckPasswordAsync(existingUser, password))
					{
						await _signInManager.SignInAsync(existingUser, false);
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
			return View();
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(string username, string password)
		{
			// TODO: test username for email address format
			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
			if (string.IsNullOrEmpty(username) || !regex.IsMatch(username))
			{
				ModelState.AddModelError("username", "The email address entered is invalid!");
			}

			if (ModelState.IsValid)
			{
				ApplicationUser registerUser = new ApplicationUser();
				registerUser.UserName = username;
				registerUser.Email = username;
				var registrationResult = await _signInManager.UserManager.CreateAsync(registerUser);

				if (registrationResult.Succeeded)
				{
					var passwordResult = await _signInManager.UserManager.AddPasswordAsync(registerUser, password);
					if (passwordResult.Succeeded)
					{
						// TODO: Send a user a message thanking them for registering.
						SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
						message.AddTo(username);
						message.Subject = "Welcome to Sharps and Trebles Music";
						message.SetFrom("sandt@sharpsandtrebles.com");
						message.AddContent("text/plain", "Thanks for registering as " + username + " on Sharps and Trebles Music!");
						message.SetTemplateId("ca66df1c-5964-43ae-bc9f-9d4ed2db5bed");
						await _sendGridClient.SendEmailAsync(message);

						await _signInManager.SignInAsync(registerUser, false);
						return RedirectToAction("Index", "Home");
					}
					foreach (var Error in passwordResult.Errors)
					{
						ModelState.AddModelError(Error.Code, Error.Description);
					}
					await _signInManager.UserManager.DeleteAsync(registerUser);
				}
				else
				{
					foreach (var Error in registrationResult.Errors)
					{
						ModelState.AddModelError(Error.Code, Error.Description);
					}
				}
			}

		//	ViewData["RegStates"] = new string[] { "Alaska","Alabama","Arkansas","American Samoa", "Arizona", "California", "Colorado", "Connecticut",
		//			"District of Columbia", "Delaware", "Florida", "Georgia", "Guam", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky",
		//			"Louisiana", "Massachusetts", "Maryland","Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina",
		//			"North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York","Ohio", "Oklahoma", "Oregon", "Pennsylvania",
		//			"Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Virgin Islands", "Vermont",
		//			"Washington","Wisconsin", "West Virginia", "Wyoming"
		//	};

			//	Register regmodel = new Register();
			//	regmodel.RegFirstName = HttpContext.Request.Form["RegFirstName"].ToString();
			//	regmodel.RegLastName = HttpContext.Request.Form["RegLastName"].ToString();
			//	regmodel.RegAddress1 = HttpContext.Request.Form["RegAddress1"].ToString();
			//	regmodel.RegAddress2 = HttpContext.Request.Form["RegAddress2"].ToString();
			//	regmodel.RegCity = HttpContext.Request.Form["RegCity"].ToString();
			//	regmodel.RegState = HttpContext.Request.Form["RegState"].ToString();
			//	regmodel.RegPostal = HttpContext.Request.Form["RegPostal"].ToString();
			//	regmodel.RegHomePhone = HttpContext.Request.Form["RegHomePhone"].ToString();
			//	regmodel.RegCellPhone = HttpContext.Request.Form["RegCellPhone"].ToString();
			//	regmodel.RegEmail = HttpContext.Request.Form["RegEmail"].ToString();
			//	regmodel.UserName = HttpContext.Request.Form["UserName"].ToString();
			//	regmodel.Password = HttpContext.Request.Form["Password"].ToString();
			//	regmodel.ConfirmPassword = HttpContext.Request.Form["ConfirmPassword"].ToString();
			//
			//	using (var connection = new System.Data.SqlClient.SqlConnection(_connectionStrings.DefaultConnection))
			//	{
			//		string query = "INSERT INTO dbo.Register(RegFirstName, RegLastName, RegAddress1, RegAddress2, RegCity, RegState, RegPostal, RegHomePhone , RegCellPhone, RegEmail, Username, Password, ConfirmPassword) values ('" + regmodel.RegFirstName + "','" + regmodel.RegLastName + "','" + regmodel.RegAddress1 + "','" + regmodel.RegAddress2 + "','" + regmodel.RegCity + "','" + regmodel.RegState + "','" + regmodel.RegPostal + "','" + regmodel.RegHomePhone + "','" + regmodel.RegCellPhone + "','" + regmodel.RegEmail + "','" + regmodel.UserName + "','" + regmodel.Password + "','" + regmodel.ConfirmPassword + "')";
			//		SqlCommand cmd = new SqlCommand(query, connection);
			//		connection.Open();
			//		int i = cmd.ExecuteNonQuery();
			//		connection.Close();
			//		result = i;
			//	}

			//	if (result > 0)
			//	{
			//		ViewBag.Result = "Data Saved Successfully";
			//	}
			//	else
			//	{
			//		ViewBag.Result = "Something Went Wrong";
			//	}
			return View();
		}

		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			var user = await _signInManager.UserManager.FindByEmailAsync(email);
			if (user != null)
			{
				string token = await _signInManager.UserManager.GeneratePasswordResetTokenAsync(user);
				string passwordResetUrl = Request.GetDisplayUrl();
				string currentUrl = Request.GetDisplayUrl();
				Uri uri = new Uri(currentUrl);
				string resetUrl = uri.GetLeftPart(UriPartial.Authority);
				resetUrl += "/account/resetpassword?id=" + token + "&email=" + email;

				SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage();
				message.AddTo(email);
				message.Subject = "Your password reset token.";
				message.SetFrom("sandt@sharpsandtrebles.com");
				message.AddContent("text/plain", resetUrl);
				message.AddContent("text/html", string.Format("<a href=\"{0} \">{0}</a>", resetUrl));
				await _sendGridClient.SendEmailAsync(message);
			}
			return RedirectToAction("ResetSent");
		}

		public IActionResult ResetSent()
		{
			return View();
		}

		public IActionResult ResetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(string id, string email, string password)
		{
			string originalToken = id;
			var user = await _signInManager.UserManager.FindByEmailAsync(email);
			if (user != null)
			{
				var result = await _signInManager.UserManager.ResetPasswordAsync(user, originalToken, password);
				if (result.Succeeded)
				{
					return RedirectToAction("Login", new { resetSuccessful = true });
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(error.Code, error.Description);
					}
				}
			}
			return View();
		}
	}
}
