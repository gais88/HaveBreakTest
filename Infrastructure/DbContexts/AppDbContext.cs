using Domian.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContexts
{
	public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>,
								IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<AppUser>()
				  .HasMany(x => x.Posts)
				  .WithOne(x => x.AppUser)
				  .HasForeignKey(x => x.AppUserId);

			builder.Entity<Post>()
				  .HasMany(x => x.Comments)
				  .WithOne(x => x.Post)
				  .HasForeignKey(x => x.PostId);
		}
	}
}
