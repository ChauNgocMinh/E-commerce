using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Entities.Users;
using E_Commerce.Domain.Entities.Roles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Infastructure.DbContexts
{
	internal class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
		{
		}
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<ProductFeedBack> ProductFeedBacks { get; set; }
		public DbSet<NumberLikeProduct> NumberLikeProducts { get; set; } 
		public DbSet<LikeFeedBack> LikeFeedBacks { get; set; } 
		public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles"); 
            });

            modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey(p => p.Id); 

				entity.Property(p => p.Name)
					  .IsRequired()
					  .HasMaxLength(200); 

				entity.Property(p => p.Price)
					  .IsRequired()
					  .HasColumnType("decimal(18,2)"); 

				entity.Property(p => p.BasePrice)
					  .IsRequired()
					  .HasColumnType("decimal(18,2)");
				
				entity.Property(p => p.Description)
					  .HasMaxLength(1000); 

				entity.HasMany(p => p.ProductImages)
					  .WithOne(pi => pi.Product)
					  .HasForeignKey(pi => pi.ProductId);

				entity.HasMany(p => p.ProductTags)
					  .WithOne(pi => pi.Product)
					  .HasForeignKey(pi => pi.ProductId);

				entity.HasMany(p => p.ProductFeedBacks)
					  .WithOne(pf => pf.Product)
					  .HasForeignKey(pf => pf.ProductId);
			});

			modelBuilder.Entity<ProductImage>(entity =>
			{
				entity.HasKey(pi => pi.Id); 

				entity.Property(pi => pi.UrlImage)
					  .IsRequired(); 

				entity.Property(pi => pi.IsMain)
					  .HasDefaultValue(false); 
			});

			modelBuilder.Entity<ProductFeedBack>(entity =>
			{
				entity.HasKey(pf => pf.Id);

				entity.Property(pf => pf.Description)
					  .IsRequired();

				entity.Property(pf => pf.Rate)
					  .IsRequired();
			});

			modelBuilder.Entity<NumberLikeProduct>(entity =>
			{
				entity.HasKey(i => i.Id); 

				entity.Property(i => i.ProductId)
					  .IsRequired(); 

				entity.Property(i => i.UserId)
					  .IsRequired(); 

				entity.HasOne(i => i.Product)
					  .WithMany() 
					  .HasForeignKey(i => i.ProductId); 

				entity.HasOne(i => i.User)
					  .WithMany() 
					  .HasForeignKey(i => i.UserId);
			});

			modelBuilder.Entity<LikeFeedBack>(entity =>
			{
				entity.HasKey(i => i.Id); 

				entity.Property(i => i.ProductFeedBackId)
					  .IsRequired(); 

				entity.Property(i => i.UserId)
					  .IsRequired(); 

				entity.HasOne(i => i.ProductFeedBack)
					  .WithMany() 
					  .HasForeignKey(i => i.ProductFeedBackId); 

				entity.HasOne(i => i.User)
					  .WithMany() 
					  .HasForeignKey(i => i.UserId);
			});

			modelBuilder.Entity<ProductTag>(entity =>
			{
				entity.HasKey(i => i.Id); 

				entity.Property(i => i.Name)
					  .HasMaxLength(30); 
			});
		}
	}
}