using BlogFitnessApp.Data;
using BlogFitnessApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BlogFitnessApp.Repositories
{
    public class BlogPostCommentRepositoryImpl : IBlogPostCommentRepository
    {

        private readonly BLogFitnessDbContext bLogFitnessDbContext;

        public BlogPostCommentRepositoryImpl(BLogFitnessDbContext bLogFitnessDbContext)
        {
            this.bLogFitnessDbContext = bLogFitnessDbContext;
        }


        public async Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment)
        {
            await bLogFitnessDbContext.BlogPostComments.AddAsync(blogPostComment);
            await bLogFitnessDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        //Obtener commentarios filtrados de usuario para los blogs 
        public async Task<IEnumerable<BlogPostComment>> GetCommentByBlogIdAsync(Guid blogPostId)
        {
            return 
                await bLogFitnessDbContext.BlogPostComments
                .Where(comment => comment.BlogPostId == blogPostId)
                .ToListAsync();
        }






    }
}
