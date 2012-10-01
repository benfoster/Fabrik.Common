using Fabrik.Common.WebAPI.AtomPub;
using Fabrik.Common.WebAPI.Links;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fabrik.Common.WebAPI.AtomPubExample
{
    public class PostModel : Resource, IPublication
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string CategoriesScheme { get; set; }

        public PostModel()
        {
            PublishDate = DateTime.UtcNow;
        }

        public PostModel(Post post, string categoriesScheme)
        {
            Id = post.Id;
            Title = post.Title;
            Slug = post.Slug;
            Summary = post.Summary;
            ContentType = post.ContentType;
            Content = post.Content;
            Tags = post.Tags;
            PublishDate = post.PublishDate;
            LastUpdated = post.LastUpdated;
            CategoriesScheme = categoriesScheme;
        }

        string IPublication.Id
        {
            get { return Links.FirstOrDefault(link => link is SelfLink).Href; }
        }

        DateTime? IPublication.PublishDate
        {
            get { return PublishDate; }
        }

        IEnumerable<IPublicationCategory> IPublication.Categories
        {
            get 
            {
                return Tags.Select(t => new PublicationCategory(t));
            }
        }
    }
}