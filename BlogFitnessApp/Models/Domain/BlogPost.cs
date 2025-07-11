﻿using BlogFitnessApp.Models.ViewModels;

namespace BlogFitnessApp.Models.Domain
{
    public class BlogPost
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

        //Navegacion proppiedad para la relacion muchos a muchos con Tag
        public ICollection<Tag> Tags { get; set; }
        public ICollection< BlogPostLike> Likes { get; set; }

        //Agregar propiedad de comentarios
        public ICollection<BlogPostComment> blogPostComments { get; set; }
    }
}
