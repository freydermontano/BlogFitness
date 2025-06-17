using System.Diagnostics;
using BlogFitnessApp.Models;
using BlogFitnessApp.Models.ViewModels;
using BlogFitnessApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogFitnessApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;

        public ITagRepository TagRepository { get; }

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogPostRepository = blogPostRepository;
            TagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            //Obtenemos todas los blog que se obtienen desde el repositorio
            var blogPosts = await blogPostRepository.GetAllAsync();

            //Obtenemos todas las etiquetas desde el repositorio
            var tags = await TagRepository.GetAllAsync();

            // instanceamos una clase , para pasar las dos coleciones 
            var model = new HomeViewModel
            {
                blogPosts = blogPosts,
                Tags = tags
            };

            //Pasamos  la clase con las dos lista de publicaciones y etiquetas
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
