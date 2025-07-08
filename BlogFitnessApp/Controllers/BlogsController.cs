using BlogFitnessApp.Models.ViewModels;
using BlogFitnessApp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;



namespace BlogFitnessApp.Controllers
{
    public class BlogsController : Controller
    {
        //Dependencias inyectadas por constructor
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BlogsController(
            IBlogPostRepository blogPostRepository,
            IBlogPostLikeRepository blogPostLikeRepository,
            //Usar autenticacion para validar algunos campos
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager

            )
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }



        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;

            // Buscar el blog por su URL
            var blogPostDetails = await blogPostRepository.GetBlogbyUrlHandleAsync(urlHandle);

            if (blogPostDetails == null)
            {
                return NotFound(); 
            }

            // Obtener total de likes (todos pueden ver esto)
            var totalLikes = await blogPostLikeRepository.GetTotalLikesAsync(blogPostDetails.Id);

            // Si el usuario está autenticado, revisamos si ya dio like
            if (signInManager.IsSignedIn(User))
            {
                var currentUser = await userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    var blogPostLikes = await blogPostLikeRepository.GetLikesForBlogAsync(blogPostDetails.Id);
                    var likeFromUser = blogPostLikes.FirstOrDefault(x =>
                        x.UserId == Guid.Parse(currentUser.Id)); 
                    liked = likeFromUser != null;
                }
            }

            // Mapear el resultado al ViewModel
            var blogDetailsViewModel = new BlogDetailsViewModel
            {
                Id = blogPostDetails.Id,
                Heading = blogPostDetails.Heading,
                PageTitle = blogPostDetails.PageTitle,
                Content = blogPostDetails.Content,
                ShortDescription = blogPostDetails.ShortDescription,
                Author = blogPostDetails.Author,
                FeaturedImageUrl = blogPostDetails.FeaturedImageUrl,
                UrlHandle = blogPostDetails.UrlHandle,
                PublishedDate = blogPostDetails.PublishedDate,
                Visible = blogPostDetails.Visible,
                Tags = blogPostDetails.Tags,
                TotalLikes = totalLikes,
                Liked = liked
            };

            return View(blogDetailsViewModel);
        }

    }
}