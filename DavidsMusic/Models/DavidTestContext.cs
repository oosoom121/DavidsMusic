using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DavidsMusic.Models
{
	public partial class DavidTestContext : IdentityDbContext<ApplicationUser>
	{
		public DavidTestContext() : base()
		{

		}

		public DavidTestContext(DbContextOptions options) : base(options)
		{

		}

		public virtual DbSet<Brand> Brand { get; set; }
		public virtual DbSet<Categories> Categories { get; set; }
		public virtual DbSet<Products> Products { get; set; }
		public virtual DbSet<ProductsCategories> ProductsCategories { get; set; }
		public virtual DbSet<ProductType> ProductType { get; set; }
		public virtual DbSet<Register> Register { get; set; }
		public virtual DbSet<CartItem> CartItems { get; set; }
		public virtual DbSet<Cart> Cart { get; set; }

		public virtual DbSet<Review> Reviews { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<LineItem> LineItems { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>().HasKey(o => o.OrderID);
			modelBuilder.Entity<Order>().Property(prop => prop.OrderID).ValueGeneratedOnAdd();
			modelBuilder.Entity<Order>().Property(prop => prop.TrackingNumber).ValueGeneratedOnAdd();
			modelBuilder.Entity<Order>().HasMany(o => o.LineItems).WithOne(l => l.Order).IsRequired();
			modelBuilder.Entity<LineItem>().HasOne(l => l.Order).WithMany(o => o.LineItems);
			modelBuilder.Entity<LineItem>().HasOne(l => l.Product).WithMany(o => o.LineItems);

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Brand>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.DateCreated)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.DateLastModified)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(100);
			});

			modelBuilder.Entity<Cart>(entity =>
			{
				entity.HasKey(e => new { e.ID });

				entity.Property(e => e.ID).HasColumnName("ID");

				entity.Property(e => e.DateCreated)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.DateLastModified)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

//				entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");
//
//				entity.HasOne(d => d.Product)
//					.WithMany(p => p.Cart)
//					.HasForeignKey(d => d.ProductId)
//					.OnDelete(DeleteBehavior.ClientSetNull)
//					.HasConstraintName("FK_CartProduct_Product");
			});

			modelBuilder.Entity<Categories>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.DateCreated)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.DateLastModified)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(100);
			});

			modelBuilder.Entity<Products>(entity =>
			{
				entity.Property(e => e.ID).HasColumnName("ID");

				entity.Property(e => e.Brand)
					.IsRequired()
					.HasMaxLength(25);

				entity.Property(e => e.DateCreated)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.DateLastModified)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.Description).HasMaxLength(1000);

				entity.Property(e => e.ImageUrl).HasMaxLength(1000);

				entity.Property(e => e.Type)
					.IsRequired()
					.HasMaxLength(25);

				entity.Property(e => e.UnitPrice).HasColumnType("money");
			});

			modelBuilder.Entity<ProductsCategories>(entity =>
			{
				entity.HasKey(e => new { e.ProductId, e.CategoryId });

				entity.Property(e => e.ProductId).HasColumnName("ProductID");

				entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
		   //
			//	entity.HasOne(d => d.Category)
			//		.WithMany(p => p.Products)
			//		.HasForeignKey(d => d.CategoryId)
			//		.OnDelete(DeleteBehavior.ClientSetNull)
			//		.HasConstraintName("FK_PRoductsCategories_Categories");
		   //
			//	entity.HasOne(d => d.Product)
			//		.WithMany(p => p.Category)
			//		.HasForeignKey(d => d.ProductId)
			//		.OnDelete(DeleteBehavior.ClientSetNull)
			//		.HasConstraintName("FK_ProductsCategories_Products");
			});

			modelBuilder.Entity<ProductType>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("ID");

				entity.Property(e => e.DateCreated)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.DateLastModified)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(100);
			});
		}
	}
}
