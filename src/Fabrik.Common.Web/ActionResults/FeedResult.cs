using System.ServiceModel.Syndication;
using System.Text;
using System.Web.Mvc;
using System.Xml;

namespace Fabrik.Common.Web
{
    public class FeedResult : ActionResult
    {
        public SyndicationFeedFormatter Formatter { get; private set; }
        public string ContentType { get; private set; }
        public Encoding Encoding { get; set; }

        public FeedResult(SyndicationFeedFormatter formatter, string contentType)
        {
            Ensure.Argument.NotNull(formatter, "feed");
            Ensure.Argument.NotNullOrEmpty(contentType, "contentType");
            
            Formatter = formatter;
            ContentType = contentType;
            Encoding = Encoding.UTF8;   
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;

            response.ContentType = ContentType;
            response.ContentEncoding = Encoding;

            using (var writer = new XmlTextWriter(response.Output))
            {
                writer.Formatting = Formatting.Indented;
                Formatter.WriteTo(writer);
            }
        }
    }
}
