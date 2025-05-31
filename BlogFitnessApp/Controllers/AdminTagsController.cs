using BlogFitnessApp.Data;
using BlogFitnessApp.Models.Domain;
using BlogFitnessApp.Models.ViewModels;
using BlogFitnessApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogFitnessApp.Controllers
{
    public class AdminTagsController : Controller
    {

        // Campo privado que representa el contexto de la base de datos
        private readonly ITagRepository _tagRepository;  


        // Constructor que recibe el contexto por inyeccion de dependencias
        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }




        [HttpGet]
        public IActionResult Add()    
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            if (!ModelState.IsValid)
                return View(addTagRequest);

            // Crea una nueva entidad Tag usando los datos recibidos desde el formulario
            var tag = new Tag
            {
               
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            // Agrega el nuevo tag a la base de datos
            await _tagRepository.AddAsync(tag); 

            //Redirigir nuevamente a la accion Add 
            return RedirectToAction("Add");  
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {

            var tags = await _tagRepository.GetAllAsync();
            return View(tags);
        }


        // Accion HTTP GET para mostrar el formulario de edicion de una etiqueta  
        public async Task<IActionResult> Edit(Guid id)
        {
            // Busca la etiqueta en la base de datos por su ID
            var tag = await _tagRepository.GetByIdAsync(id);

            if ( tag != null)
            {
                // Crea un objeto del modelo que necesita la vista (EditTagRequest)
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };


                return View(editTagRequest);
            }

            return View(null);
        }

        // Accion HTTP POST que recibe el formulario para actualizar una etiqueta
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {

            // Crear entidad Tag desde el modelo de vista
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            // Llama al repositorio para actualizar
            var updatedTag = await _tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                // Redirige nuevamente a la pagina de edición con mensaje de exito 
                return RedirectToAction("Edit", new { id = tag.Id });
            }

            return RedirectToAction("Edit", new { id = tag.Id });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {

            var deleteTag = await _tagRepository.DeleteAsync(editTagRequest.Id);

            if (deleteTag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new {id = editTagRequest.Id});
        }



    }
}
