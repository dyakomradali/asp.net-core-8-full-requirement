using fullrequirementproject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fullrequirementproject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<PostModels> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }   

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(p => p.Posts)
                .WithOne(u => u.applicationUser)
                .HasForeignKey(u => u.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(d=>d.Comments)
                .WithOne(u=>u.applicationUser)
                .HasForeignKey(u => u.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PostModels>()
                .HasMany(c=>c.Comments)
                .WithOne(p=>p.PostModels)
                .HasForeignKey(p=>p.PostId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
