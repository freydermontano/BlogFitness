using BlogFitnessApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogFitnessApp.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }




        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle) 
        {
            var blogPostDetails = await blogPostRepository.GetBlogbyUrlHandleAsync(urlHandle);

            return View(blogPostDetails);
        }
    }
}
