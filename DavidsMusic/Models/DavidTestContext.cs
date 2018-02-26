using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DavidsMusic.Models
{
	public partial class DavidTestContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApplicationUser>
	{

		public DavidTestContext() : base()
		{

		}

		public DavidTestContext(DbContextOptions options) : base(options)
		{

		}

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductsCategories> ProductsCategories { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }

        // Unable to generate entity type for table 'dbo.RegisteredCustomers'. Please see the warning messages.

   //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //     {
   //         if (!optionsBuilder.IsConfigured)
   //         {
   //             optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DavidTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
   //         }
   //     }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                entity.HasKey(e => new { e.CartId, e.ProductId });

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateLastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartProduct_Product");
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
                entity.Property(e => e.Id).HasColumnName("ID");

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

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductsCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRoductsCategories_Categories");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsCategories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsCategories_Products");
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
