using BlogFitnessApp.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogFitnessApp.Models.ViewModels
{
    public class BlogDetailsViewModel
    {

        public Guid Id { get; set; }
        public string Heading { get; set; }

        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string Author { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Visible { get; set; }

        public ICollection<Tag> Tags { get; set; }

        //Propiedad likes 
        public int TotalLikes { get; set; }

        //Propiedad para verificar si el usuario ha dado like al blog post
        public bool Liked { get; set; }  
    }
}
