
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BlogFitnessApp.Repositories
{
    public class ClaudinaryImageRepositoryImpl : IImageRepository
    {


        private readonly IConfiguration configuration;// para acceder a la configuración de mi aplicacion (appsettings.json, secrets, variables de entorno, etc.)
        private readonly Account account; //representa una cuenta de Cloudinary. Necesita tres datos: CloudName, ApiKey y ApiSecret


        // Constructor que recibe la configuración por inyección de dependencias
        //Guarda el objeto IConfiguration en una propiedad de la clase
        public ClaudinaryImageRepositoryImpl(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]
                );
        }


        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName,

            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString(); // Devuelve la URL segura de la imagen subida
            }
            else
            {
                throw new Exception("Error uploading image: " + uploadResult.Error?.Message);
            }

            return null;
        }
    }
}
