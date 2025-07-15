using BlogFitnessApp.Models.ViewModels;

namespace BlogFitnessApp.Repositories
{
    public interface IBlogPostCommentRepository
    {

        Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetCommentByBlogIdAsync(Guid blogPostId); 
    }
}
