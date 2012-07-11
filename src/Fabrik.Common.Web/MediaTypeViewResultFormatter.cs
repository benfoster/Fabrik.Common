using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// Provides a base class for formatters that use the media type to determine if they should format the result.
    /// </summary>
    public abstract class MediaTypeViewResultFormatter : IViewResultFormatter
    {
        private readonly List<string> supportedMediaTypes = new List<string>();
        public IEnumerable<string> SupportedMediaTypes { get { return supportedMediaTypes; } }

        protected void AddSupportedMediaType(string mediaType)
        {
            Ensure.Argument.NotNullOrEmpty(mediaType, "mediaType");
            supportedMediaTypes.Add(mediaType);
        }

        public virtual ActionResult CreateResult(ControllerContext controllerContext, ActionResult current)
        {
            return CreateResult(controllerContext);
        }

        public virtual bool IsSatisfiedBy(ControllerContext controllerContext)
        {
            Ensure.Argument.NotNull(controllerContext, "controllerContext");

            var acceptTypes = controllerContext.HttpContext.Request.AcceptTypes;
            return acceptTypes.Intersect(supportedMediaTypes).Any();
        }

        public abstract ActionResult CreateResult(ControllerContext controllerContext);
    }
}
