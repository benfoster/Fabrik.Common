using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public abstract class ViewResultFormatter : IViewResultFormatter
    {
        private readonly List<string> supportedMediaTypes = new List<string>();
        public IEnumerable<string> SupportedMediaTypes { get { return supportedMediaTypes; } }

        protected void AddSupportedMediaType(string mediaType)
        {
            Ensure.Argument.NotNullOrEmpty(mediaType, "mediaType");
            supportedMediaTypes.Add(mediaType);
        }

        public abstract ActionResult CreateResult(ControllerContext controllerBase);

        public virtual bool IsSatisfiedBy(HttpContextBase httpContext)
        {
            Ensure.Argument.NotNull(httpContext, "httpContext");
            
            var acceptTypes = httpContext.Request.AcceptTypes;
            return acceptTypes.Intersect(supportedMediaTypes).Any();
        }
    }
}
