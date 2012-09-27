using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Web.Http;
using System.Xml;

namespace Fabrik.Common.WebAPI.AtomPubExample.Controllers
{
    public class ServicesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var doc = new ServiceDocument();
            var ws = new Workspace
            {
                Title = new TextSyndicationContent("My Site"),
                BaseUri = new Uri(Request.RequestUri.GetLeftPart(UriPartial.Authority))
            };

            var posts = new ResourceCollectionInfo("Blog",
                new Uri(Url.Link("DefaultApi", new { controller = "posts" })));

            posts.Accepts.Add("application/atom+xml;type=entry");

            ws.Collections.Add(posts);
            doc.Workspaces.Add(ws);

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            var formatter = new AtomPub10ServiceDocumentFormatter(doc);

            var stream = new MemoryStream();
            using (var writer = XmlWriter.Create(stream))
            {
                formatter.WriteTo(writer);
            }

            stream.Position = 0;
            var content = new StreamContent(stream);
            response.Content = content;
            response.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/atomsvc+xml");

            return response;
        }
    }
}
