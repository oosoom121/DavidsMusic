﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.SqlClient;


namespace DavidsMusic.Models
{
    public class Register
    {
		public int ID { get; set; }
		[Required(ErrorMessage = "First name is required.")]
		[Display(Name = "First Name")]
		public string RegFirstName { get; set; }
		[Required(ErrorMessage = "Last name is required.")]
		[Display(Name = "Last Name")]
		public string RegLastName { get; set; }
		[Display(Name = "Address")]
		public string RegAddress1 { get; set; }
		[Display(Name = "Address 2")]
		public string RegAddress2 { get; set; }
		[Display(Name = "City")]
		public string RegCity { get; set; }
		[Display(Name = "State")]
		public string RegState { get; set; }
		[Display(Name = "Postal Code")]
		public string RegPostal { get; set; }
		[Display(Name = "Home Phone")]
		public string RegHomePhone { get; set; }
		[Display(Name = "Cell Phone")]
		public string RegCellPhone { get; set; }
		[Required(ErrorMessage = "An Email Address is required.")]
		[Display(Name = "Email Address")]
		public string RegEmail { get; set; }
		[Required(ErrorMessage = "Username is required.")]
		[Display(Name = "Username")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Password is required.")]
		[Display(Name = "Password")]
		public string Password { get; set; }
		[Compare("Password", ErrorMessage = "Password not match.")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

	}
}

