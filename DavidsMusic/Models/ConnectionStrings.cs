using Microsoft.Extensions.Configuration;
using System.IO;

namespace DavidsMusic.Models
{
    public class ConnectionStrings
    {
		public string DefaultConnection { get; set; }
	//
	//	public static string ConString()
	//	{
	//		var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
	//		var config = builder.Build();
	//		string constring = config.GetConnectionString("DavidTest");
	//		return constring;
	//	}

	}
}
