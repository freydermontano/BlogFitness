using BlogFitnessApp.Data;
using BlogFitnessApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogFitnessApp.Repositories
{
    public class BlogPostRepositoryImpl : IBlogPostRepository  
    {


        // Contexto de la base de datos
        private readonly BLogFitnessDbContext bLogFitnessDbContext;

        // Constructor que recibe el contexto por inyeccion de dependencias
        public BlogPostRepositoryImpl( BLogFitnessDbContext bLogFitnessDbContext )
        {
           this.bLogFitnessDbContext = bLogFitnessDbContext;
        }



        public async Task<BlogPost?> AddAsync(BlogPost blogPost)
        {
            await bLogFitnessDbContext.AddAsync(blogPost);
            await bLogFitnessDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await bLogFitnessDbContext.BlogPosts.FindAsync(id);

            if (existingBlogPost != null)
            {
               bLogFitnessDbContext.BlogPosts.Remove(existingBlogPost);
               await bLogFitnessDbContext.SaveChangesAsync();
               return existingBlogPost;
            }

            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await bLogFitnessDbContext.BlogPosts.ToListAsync();
        }

        public Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return bLogFitnessDbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = bLogFitnessDbContext.BlogPosts.Find(blogPost.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Visible = blogPost.Visible;

                await bLogFitnessDbContext.SaveChangesAsync();

                return existingBlogPost;
            }

            return null;
        }
    }
}
