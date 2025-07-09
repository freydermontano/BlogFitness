using BlogFitnessApp.Data;
using BlogFitnessApp.Models.ViewModels;

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
    }
}
