using BlogFitnessApp.Models.Domain;
using Microsoft.Extensions.Hosting;

namespace BlogFitnessApp.Models.ViewModels
{
    public class HomeViewModel
    {

        public IEnumerable<BlogPost> blogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}


//posts y tags, necesitas combinarlas en una sola clase. para mostrar en la vista
// Por que a la vista se le pasa solo un modelo, y no dos modelos diferentes