using E_Commerce.Domain.Entities.Movies;
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
		public DbSet<Movie> Movies { get; set; }
		public DbSet<MovieCategory> MovieCategorys { get; set; }
		public DbSet<MovieImage> MovieImages { get; set; }
		public DbSet<MovieFeedBack> MovieFeedBacks { get; set; }
		public DbSet<MovieTag> MovieTags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles"); 
            });

            modelBuilder.Entity<Movie>(entity =>
			{
				entity.HasKey(p => p.Id); 

				entity.Property(p => p.Name)
					  .IsRequired()
					  .HasMaxLength(200); 
				
				entity.Property(p => p.Description)
					  .HasMaxLength(1000); 

				entity.HasMany(p => p.MovieImages)
					  .WithOne(pi => pi.Movie)
					  .HasForeignKey(pi => pi.MovieId);

				entity.HasMany(p => p.MovieTags)
					  .WithOne(pi => pi.Movie)
					  .HasForeignKey(pi => pi.MovieId);

				entity.HasMany(p => p.MovieFeedBacks)
					  .WithOne(pf => pf.Movie)
					  .HasForeignKey(pf => pf.MovieId);
			});


			modelBuilder.Entity<MovieCategory>(entity =>
			{
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Title)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.Description);

                entity.HasMany(p => p.Movies)
                      .WithOne(pf => pf.MovieCategory)
                      .HasForeignKey(pf => pf.CategoryId);
            });

            modelBuilder.Entity<MovieImage>(entity =>
			{
				entity.HasKey(pi => pi.Id); 

				entity.Property(pi => pi.UrlImage)
					  .IsRequired(); 

				entity.Property(pi => pi.IsMain)
					  .HasDefaultValue(false); 
			});

			modelBuilder.Entity<MovieFeedBack>(entity =>
			{
				entity.HasKey(pf => pf.Id);

				entity.Property(pf => pf.Comment);

				entity.Property(pf => pf.Rate)
					  .IsRequired();
			});

			modelBuilder.Entity<MovieTag>(entity =>
			{
				entity.HasKey(i => i.Id); 

				entity.Property(i => i.Name)
					  .HasMaxLength(30); 
			});
		}
	}
}