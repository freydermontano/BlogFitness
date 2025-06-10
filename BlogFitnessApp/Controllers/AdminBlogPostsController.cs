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




        [HttpGet]
        public async Task<IActionResult> List()
        {

            var blogPosts = await _blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }


        //Accion que recibe id para editar blog post
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Busca la etiqueta en la base de datos por su ID
            var blogPost = await _blogPostRepository.GetByIdAsync(id);

            //repositorio de tags para obtener los tags
            var tagsDomainModel = await _tagRepository.GetAllAsync();

            if (blogPost  != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    ShortDescription = blogPost.ShortDescription,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,

                    //para traer los tags asociados al blog post, usamos el repositorio de tags
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.DisplayName,
                        Selected = blogPost.Tags.Any(tag => x.Id == x.Id) // Marca el tag como seleccionado si está asociado al blog post
                    }),

                    //para traer los tags seleccionados en el formulario, usamos la propiedad SelectedTag del modelo de vista
                    SelectedTag = blogPost.Tags.Select(t => t.Id.ToString()).ToArray()

                };

                return View(model);
            }

            return View(null);
        }


        //Editar cambios
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {

            //Mapear modelo vista debuelta al modelo dominio

            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                ShortDescription = editBlogPostRequest.ShortDescription,
                Author = editBlogPostRequest.Author,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                UrlHandle = editBlogPostRequest.UrlHandle,
                PublishedDate = editBlogPostRequest.PublishedDate,
                Visible = editBlogPostRequest.Visible,
            };

            //Mapear las etiquetas dentro del dominio modelo
            var selectedTags = new List<Tag>();

            foreach (var selectedTag in editBlogPostRequest.SelectedTag)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await _tagRepository.GetByIdAsync(tag);
                    //agregar tag
                    if (foundTag != null)
                    {
                        selectedTags.Add(foundTag);
                    }
                }
            }

            //pasamos la informacion al modelo dominio
            blogPostDomainModel.Tags = selectedTags;




            // Llama al repositorio para actualizar y guardar la informacion actualizada
            var updateBlogPost = await _blogPostRepository.UpdateAsync(blogPostDomainModel);

            if (updateBlogPost != null)
            {
                //mostrar notificacion exitosa
                return RedirectToAction("Edit");
            }
            //mostrar notificacion error
            return RedirectToAction("Edit");      
        }


        //Eliminar blog post
        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {

            // Llama al repositorio para eliminar el blog post por su ID
            var deletedBlogPost = await _blogPostRepository.DeleteAsync(editBlogPostRequest.Id);

            if (deletedBlogPost != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new {id = deletedBlogPost.Id});   
        }


    }
}
