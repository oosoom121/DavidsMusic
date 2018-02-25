using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization.Formatters.Binary;
using DavidsMusic.Models;
using System.Data.SqlClient;

namespace DavidsMusic.Controllers
{
	public class RegisterController : Controller
	{
		int result = 0;
		private Models.ConnectionStrings _connectionStrings;
		public RegisterController(Microsoft.Extensions.Options.IOptions<Models.ConnectionStrings> connectionStrings)
		{
			_connectionStrings = connectionStrings.Value;
		}

		public IActionResult Register()
		{
			ViewData["RegStates"] = new string[] { "Alaska","Alabama","Arkansas","American Samoa", "Arizona", "California", "Colorado", "Connecticut",
					"District of Columbia", "Delaware", "Florida", "Georgia", "Guam", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky",
					"Louisiana", "Massachusetts", "Maryland","Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina",
					"North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York","Ohio", "Oklahoma", "Oregon", "Pennsylvania",
					"Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Virgin Islands", "Vermont",
					"Washington","Wisconsin", "West Virginia", "Wyoming"
			};
			return View();
		}

		[HttpPost]
		public IActionResult RegCustDetails()
		{
			ViewData["RegStates"] = new string[] { "Alaska","Alabama","Arkansas","American Samoa", "Arizona", "California", "Colorado", "Connecticut",
					"District of Columbia", "Delaware", "Florida", "Georgia", "Guam", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky",
					"Louisiana", "Massachusetts", "Maryland","Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina",
					"North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York","Ohio", "Oklahoma", "Oregon", "Pennsylvania",
					"Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Virgin Islands", "Vermont",
					"Washington","Wisconsin", "West Virginia", "Wyoming"
			};

			RegisterModel regmodel = new RegisterModel();
			regmodel.RegFirstName = HttpContext.Request.Form["RegFirstName"].ToString();
			regmodel.RegLastName = HttpContext.Request.Form["RegLastName"].ToString();
			regmodel.RegAddress1 = HttpContext.Request.Form["RegAddress1"].ToString();
			regmodel.RegAddress2 = HttpContext.Request.Form["RegAddress2"].ToString();
			regmodel.RegCity = HttpContext.Request.Form["RegCity"].ToString();
			regmodel.RegState = HttpContext.Request.Form["RegState"].ToString();
			regmodel.RegPostal = HttpContext.Request.Form["RegPostal"].ToString();
			regmodel.RegHomePhone = HttpContext.Request.Form["RegHomePhone"].ToString();
			regmodel.RegCellPhone = HttpContext.Request.Form["RegCellPhone"].ToString();
			regmodel.RegEmail = HttpContext.Request.Form["RegEmail"].ToString();
			regmodel.UserName = HttpContext.Request.Form["UserName"].ToString();
			regmodel.Password = HttpContext.Request.Form["Password"].ToString();
			regmodel.ConfirmPassword = HttpContext.Request.Form["ConfirmPassword"].ToString();

			using (var connection = new System.Data.SqlClient.SqlConnection(_connectionStrings.DefaultConnection))
			{
				string query = "INSERT INTO dbo.RegisteredCustomers(FirstName, LastName, Address1, Address2, City, State, PostalCode, HomePhone , CellPhone, EmailAddress, Username, UserPassword, ConfirmPassword) values ('" + regmodel.RegFirstName + "','" + regmodel.RegLastName + "','" + regmodel.RegAddress1 + "','" + regmodel.RegAddress2 + "','" + regmodel.RegCity + "','" + regmodel.RegState + "','" + regmodel.RegPostal + "','" + regmodel.RegHomePhone + "','" + regmodel.RegCellPhone + "','" + regmodel.RegEmail + "','" + regmodel.UserName + "','" + regmodel.Password + "','" + regmodel.ConfirmPassword + "')";
				SqlCommand cmd = new SqlCommand(query, connection); 
				connection.Open();
				int i = cmd.ExecuteNonQuery();
				connection.Close();
				result = i;
			}

			if (result > 0)
			{
				ViewBag.Result = "Data Saved Successfully";
			}
			else
			{
				ViewBag.Result = "Something Went Wrong";
			}
			return View("Register");
		}
	}
}