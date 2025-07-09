using BlogFitnessApp.Models.ViewModels;

namespace BlogFitnessApp.Repositories
{
    public interface IBlogPostCommentRepository
    {

        Task<BlogPostComment> AddCommentAsync(BlogPostComment blogPostComment);
    }
}
