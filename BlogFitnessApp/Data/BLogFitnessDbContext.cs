using BlogFitnessApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogFitnessApp.Data
{
    public class BLogFitnessDbContext : DbContext
    {
        public BLogFitnessDbContext(DbContextOptions<BLogFitnessDbContext> options) : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }

    }
}
