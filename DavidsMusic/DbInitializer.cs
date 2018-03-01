using DavidsMusic.Models;
using Microsoft.EntityFrameworkCore;
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
			context.Database.Migrate();

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
					StockNumber = 12340,
					Description = "Gibson Les Paul\nGibson Les Paul Electric guitar.",
					ImageUrl = "/images/LP-2.png",
					UnitPrice = 1089.00M,
				//	Category = context.Categories.First(x => x.Name == "Instruiments")
				});
				context.Products.Add(new Products
				{
					Brand = "Gibson",
					Type = "Guitar",
					StockNumber = 12341,
					Description = "Gibson Les Paul\nGibson VOS '57Les Paul Goldtop (Reissue).",
					ImageUrl = "/images/LP-3.png",
					UnitPrice = 3255.00M,
				//	Category = context.Categories.First(x => x.Name == "Instruiments")

				});
				context.Products.Add(new Products
				{
					Brand = "Gibson",
					Type = "Guitar",
					StockNumber = 12342,
					Description = "Gibson Les Paul\nGibson-Historic-’59-Les-Paul-Reissue-VOS.",
					ImageUrl = "/images/LP-4.png",
					UnitPrice = 3025.00M,
				//	Category = context.Categories.First(x => x.Name == "Instruiments")
				});

				context.Products.Add(new Products
				{
					Brand = "Gibson",
					Type = "Guitar",
					StockNumber = 12343,
					Description = "Gibson Les Paul\nGibson Custom 1959 Les Paul Standard Reissue.",
					ImageUrl = "/images/LP-5.png",
					UnitPrice = 2855.00M,
				//	Category = context.Categories.First(x => x.Name == "Instruiments")
				});

				context.Products.Add(new Products
				{
					Brand = "Yamaha",
					Type = "Piano",
					StockNumber = 23456,
					Description = "Yamaha accoustic upright piano.",
					ImageUrl = "/images/yampiano.jpg",
					UnitPrice = 1549.99M,
				//	Category = context.Categories.First(x => x.Name == "Instruiments")
				});
				context.SaveChanges();
			}

			if (!context.Reviews.Any())
			{
				context.Reviews.Add(new Review
				{
					Rating = 5,
					Body = "Best guitar I've ever had!",
					IsApproved = true,
					Product = context.Products.First()

				});
				context.Reviews.Add(new Review
				{
					Rating = 3,
					Body = "Not bad but I've played better.",
					IsApproved = true,
					Product = context.Products.First()

				});
				context.SaveChanges();
			}
		}
	}
}
