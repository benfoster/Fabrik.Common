using Fabrik.Common.WebAPI.Links;
using System;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Fabrik.Common.WebAPI.AtomPubExample
{
    public class PostResponseEnricher : IResponseEnricher
    {
        public bool CanEnrich(HttpResponseMessage response)
        {
            var content = response.Content as ObjectContent;

            return content != null
                && (content.ObjectType == typeof(PostModel) || content.ObjectType == typeof(PostFeed));
        }

        public HttpResponseMessage Enrich(HttpResponseMessage response)
        {
            PostModel post;

            var urlHelper = response.RequestMessage.GetUrlHelper();

            if (response.TryGetContentValue<PostModel>(out post))
            {
                Enrich(post, urlHelper);
                return response;
            }

            PostFeed feed;
            if (response.TryGetContentValue<PostFeed>(out feed))
            {
                feed.Posts.ForEach(p => Enrich(p, urlHelper));
                var selfUrl = urlHelper.Link("DefaultApi", new { controller = "posts" });
                feed.AddLink(new SelfLink(selfUrl));
            }

            return response;
        }

        private void Enrich(PostModel post, UrlHelper url)
        {
            var selfUrl = url.Link("DefaultApi", new { controller = "posts", id = post.Id });
            post.AddLink(new SelfLink(selfUrl));
            post.AddLink(new EditLink(selfUrl));
        }
    }
}