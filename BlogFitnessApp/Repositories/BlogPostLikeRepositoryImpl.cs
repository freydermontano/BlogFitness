
using BlogFitnessApp.Data;
using BlogFitnessApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogFitnessApp.Repositories
{
    public class BlogPostLikeRepositoryImpl : IBlogPostLikeRepository
    {
        private readonly BLogFitnessDbContext bLogFitnessDbContext;

        public BlogPostLikeRepositoryImpl(BLogFitnessDbContext bLogFitnessDbContext)
        {
            this.bLogFitnessDbContext = bLogFitnessDbContext;
        }

        public async Task<BlogPostLike> AddLikesForBlogAsync(BlogPostLike blogPostId)
        {
            await bLogFitnessDbContext.BlogPostLikes.AddAsync(blogPostId);
            await bLogFitnessDbContext.SaveChangesAsync();
            return blogPostId;

        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlogAsync(Guid blogPostId)
        {
           return await bLogFitnessDbContext.BlogPostLikes.Where(x => x.BlogPostId  == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikesAsync(Guid blogPostId)
        {
             return  await bLogFitnessDbContext.BlogPostLikes.
                CountAsync(x => x.BlogPostId == blogPostId);   

        }
    }
}
