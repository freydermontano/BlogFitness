using BlogFitnessApp.Models.Domain;
using BlogFitnessApp.Models.ViewModels;
using BlogFitnessApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogFitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {


        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {

            var modelBlogPostLike = new  BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId
            };

            var result = await blogPostLikeRepository.AddLikesForBlogAsync(modelBlogPostLike); 

            if (result != null)
            {
                return Ok(new { message = "Like agregado Exitosamente", data = result });
            }
            else
            {
                return BadRequest(new { message = "Fallo al agregar like" });
            }

        }




        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {

            var totalLikes = await blogPostLikeRepository.GetTotalLikesAsync(blogPostId);
            return Ok(totalLikes);

        }
    }
}
