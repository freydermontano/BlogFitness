using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogFitnessApp.Models.ViewModels
{
    public class EditBlogPostRequest
    {

        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string Author { get; set; }
        public string FeaturedImageUrl { get; set; } = string.Empty;
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Visible { get; set; }


        // Propiedad para los tags asociados al blog post
        public IEnumerable<SelectListItem> Tags { get; set; }

        // Propiedad para los tags seleccionados en el formulario
        public string[] SelectedTag { get; set; } = Array.Empty<string>();

    }
}
