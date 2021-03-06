﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidsMusic.Models
{
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
	{
		public ApplicationUser()
		{
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FavoriteInstrument { get; set; }

		public ICollection<Review> Reviews { get; set; }
	}
}
