using BlogFitnessApp.Models.Domain;
using BlogFitnessApp.Models.ViewModels;
using BlogFitnessApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogFitnessApp.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        public readonly ITagRepository _tagRepository;
        public readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Obtener tags desde la base de datos y pasarlos al modelo de vista
            var tags = await _tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.DisplayName
                }),
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {

            //mapear los datos del modelo de vista a la entidad BlogPost
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                Author = addBlogPostRequest.Author,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Visible = addBlogPostRequest.Visible
            };

            //Mapear el tag seleccionado al blog post
            //Se crea una lista vacía de objetos Tag que se llenara con los tags seleccionados por el usuario en el formulario.
            var selectedTags = new List<Tag>();

           //Se recorren los IDs de los tags seleccionados en el formulario(SelectedTag).
           foreach (var tagId in addBlogPostRequest.SelectedTag.Split(',').Where(id => !string.IsNullOrEmpty(id)))
            {
                //Se busca cada tag en la base de datos por su ID.
                var tag = await _tagRepository.GetByIdAsync(Guid.Parse(tagId));
                //Si el tag existe, se agrega a la lista de tags seleccionados.
                if (tag != null)
                {
                    selectedTags.Add(tag);
                }
            }

            //Mapear los tags seleccionados al blog post
            blogPost.Tags = selectedTags;

            //Se asigna la lista de tags seleccionados a la propiedad Tags del objeto blogPost.
            await _blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("Add");
        }
    }
}
