using BlogFitnessApp.Models.Domain;

namespace BlogFitnessApp.Repositories
{
    public interface IBlogPostLikeRepository
    {

        //Cantidad de like en el blog 
        Task<int> GetTotalLikesAsync(Guid blogPostId);

        //Obtener likes si el usuario esta logeado
        Task<IEnumerable<BlogPostLike>> GetLikesForBlogAsync(Guid blogPostId);

        //Agregar like al blog
        Task<BlogPostLike> AddLikesForBlogAsync(BlogPostLike blogPostId);
    }
}
