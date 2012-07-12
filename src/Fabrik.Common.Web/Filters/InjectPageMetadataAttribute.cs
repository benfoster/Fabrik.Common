using System.Web.Configuration;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public class InjectPageMetadataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;

            if (viewResult == null) return;

            var settings = WebConfigurationManager.AppSettings;

            var title = viewResult.ViewBag.Title ?? settings["MetaTitle"];
            var description = viewResult.ViewBag.MetaDescription ?? settings["MetaDescription"];
            var keywords = viewResult.ViewBag.MetaKeywords ?? settings["MetaKeywords"];

            var metadata = viewResult.Model as PageMetadata;

            if (metadata != null)
            {
                title = metadata.PageTitle ?? title;
                description = metadata.MetaDescription ?? description;
                keywords = metadata.MetaKeywords ?? keywords;
            }

            viewResult.ViewBag.Title = title;
            viewResult.ViewBag.MetaDescription = description;
            viewResult.ViewBag.MetaKeywords = keywords;

            base.OnActionExecuted(filterContext);
        }
    }
}
