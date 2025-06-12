using BlogFitnessApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogFitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        private readonly IImageRepository imageRepository;


        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }



        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
        {
            //Llamar al repositorio de imagenes para subir la imagen

            var imageURL = await imageRepository.UploadImageAsync(file);
            if (imageURL == null)
            {
                return Problem("Algo Salio mal", null, (int)HttpStatusCode.InternalServerError);  
            }

            return new JsonResult(new { link = imageURL });

        }





    }
}
