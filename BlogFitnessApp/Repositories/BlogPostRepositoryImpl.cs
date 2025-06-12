using BlogFitnessApp.Data;
using BlogFitnessApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
            //Include(x => x.Tags) le dice a EF Core: Cuando obtengas los blog posts,
            //tambien trae los tags que estan relacionados con cada uno.
            return await bLogFitnessDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return bLogFitnessDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await bLogFitnessDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id); 

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
                existingBlogPost.Tags = blogPost.Tags;

                await bLogFitnessDbContext.SaveChangesAsync();

                //retornar el blog actualizado
                return existingBlogPost;
            }

            return null;
        }
    }
}
