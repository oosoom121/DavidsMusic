using DavidsMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidsMusic
{
	internal class DbInitializer
	{


		internal static void Initialize(DavidTestContext context)
		{
			context.Database.EnsureCreated();

			if (!context.Categories.Any())
			{
				context.Categories.Add(new Categories
				{
					Name = "Instruments",
					DateCreated = DateTime.Now,
					DateLastModified = DateTime.Now,
					//Description = "This is an instrument category",
				});

				context.Categories.Add(new Categories
				{
					Name = "Accessories",
					DateCreated = DateTime.Now,
					DateLastModified = DateTime.Now
					//Description = "This is an accessory category",
				});
				context.SaveChanges();
			}

			if (!context.Products.Any())
			{
				context.Products.Add(new Products
				{
					Brand = "Gibson",
					Type = "Guitar",
					StockNumber = 12345,
					Description = "Fireburst Les Paul Standard.",
					ImageUrl = "/images/LP-1.png",
					UnitPrice = 549.99M
				});
				context.Products.Add(new Products
				{
					Brand = "Yamaha",
					Type = "Piano",
					StockNumber = 23456,
					Description = "Yamaha accoustic upright piano.",
					ImageUrl = "/images/yampiano.jpg",
					UnitPrice = 1549.99M
				});
				context.SaveChanges();
			}

		}
	}
}
