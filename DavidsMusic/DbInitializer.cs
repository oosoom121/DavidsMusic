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
					Name = "Guitars",
					DateCreated = DateTime.Now,
					DateLastModified = DateTime.Now,
					//Description = "This is an instrument category",
				});

				context.Categories.Add(new Categories
				{
					Name = "Keyboards",
					DateCreated = DateTime.Now,
					DateLastModified = DateTime.Now
					//Description = "This is an accessory category",
				});

				context.Categories.Add(new Categories
				{
					Name = "Percussion",
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
					Category = context.Categories.First(x => x.Id == 1)
				});
				context.Products.Add(new Products
				{
					Brand = "Casio",
					Type = "Piano",
					StockNumber = 7807,
					Description = "Casio Privia PX-780 features a new redesigned 88 note Tri-sensor scaled hammer action keyboard. It reproduce the sound of the finest acoustic grand pianos.",
					ImageUrl = "/images/caspx780cb7.png",
					UnitPrice = 1399.99M,
					Category = context.Categories.First(x => x.Id == 2)

				});
				context.Products.Add(new Products
				{
					Brand = "Gibson",
					Type = "Guitar",
					StockNumber = 12342,
					Description = "Gibson Les Paul\nGibson-Historic-’59-Les-Paul-Reissue-VOS.",
					ImageUrl = "/images/LP-4.png",
					UnitPrice = 3025.00M,
					Category = context.Categories.First(x => x.Id == 1)
				});

				context.Products.Add(new Products
				{
					Brand = "ROLAND ",
					Type = "Piano",
					StockNumber = 12603,
					Description = "Roland DP-603 - A SLIM AND STYLISH DIGITAL PIANO!  Features Roland’s latest SuperNATURAL Piano Modeling for rich, authentic sounds and a PHA - 50 keyboard that feels so expressive to play.",
					ImageUrl = "/images/RolakdDp603.png",
					UnitPrice = 2855.00M,
					Category = context.Categories.First(x => x.Id == 2)
				});

				context.Products.Add(new Products
				{
					Brand = "Yamaha",
					Type = "Piano",
					StockNumber = 23456,
					Description = "Yamaha accoustic upright piano.",
					ImageUrl = "/images/yampiano.jpg",
					UnitPrice = 1549.99M,
					Category = context.Categories.First(x => x.Id == 2)
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
