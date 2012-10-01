using System;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Fabrik.Common.WebAPI.AtomPub
{
    public static class AtomExtensions
    {
        public static SyndicationFeed Syndicate(this IPublicationFeed feed)
        {
            Ensure.Argument.NotNull(feed, "feed");

            var atomFeed = new SyndicationFeed
            {
                Title = new TextSyndicationContent(feed.Title),
                Items = feed.Items.Select(i => i.Syndicate()),
                Description = new TextSyndicationContent(feed.Summary ?? string.Empty)
            };

            atomFeed.Authors.Add(new SyndicationPerson { Name = feed.Author });

            feed.Links.ForEach(link =>
                atomFeed.Links.Add(new SyndicationLink(new Uri(link.Href)) { RelationshipType = link.Rel, Title = link.Title }));

            return atomFeed;
        }
        
        public static SyndicationItem Syndicate(this IPublication publication)
        {
            Ensure.Argument.NotNull(publication, "publication");

            var item = new SyndicationItem
            {
                Id = publication.Id,
                Title = new TextSyndicationContent(publication.Title, TextSyndicationContentKind.Plaintext),
                LastUpdatedTime = publication.PublishDate ?? publication.LastUpdated, // use publish date if it exists (for posts)
                Summary = new TextSyndicationContent(publication.Summary, TextSyndicationContentKind.Plaintext),
                Content = GetSyndicationContent(publication.Content, publication.ContentType)
            };

            if (publication.PublishDate.HasValue) // Optional according to Atom spec
            {
                item.PublishDate = publication.PublishDate.Value;
            }

            publication.Categories.ForEach(category =>
                item.Categories.Add(new SyndicationCategory(category.Name, publication.CategoriesScheme, category.Label)));

            publication.Links.ForEach(link =>
                item.Links.Add(new SyndicationLink(new Uri(link.Href)) { RelationshipType = link.Rel, Title = link.Title }));

            return item;
        }

        public static void ReadSyndicationItem<TCommand>(this TCommand command, SyndicationItem item)
            where TCommand : IPublicationCommand
        {
            Ensure.Argument.NotNull(command, "command");
            Ensure.Argument.NotNull(item, "item");

            command.Title = item.Title.Text;
            command.Summary = item.Summary != null ? item.Summary.Text : null;
            command.Content = ((TextSyndicationContent)item.Content).Text;
            command.ContentType = item.Content.Type;
            command.Categories = item.Categories.Select(c => c.Name).ToArray();
            command.PublishDate = GetPublishDate(item.PublishDate);
        }

        private static SyndicationContent GetSyndicationContent(string content, string contentType)
        {
            if (content.IsNullOrEmpty() || contentType.ToLowerInvariant() == PublicationContentTypes.Text)
                return SyndicationContent.CreatePlaintextContent(content ?? string.Empty);

            return SyndicationContent.CreateHtmlContent(content);
        }

        public static DateTime GetPublishDate(DateTimeOffset syndicationDate)
        {
            var publishDate = syndicationDate.UtcDateTime;
            // if the publish date has not been set it will be equal to DateTime.MinValue
            return publishDate == DateTime.MinValue ? DateTime.UtcNow : publishDate;
        }
    }
}