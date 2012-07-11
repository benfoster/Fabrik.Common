using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public static class ViewResultFormatters
    {
        public static IList<IViewResultFormatter> Formatters { get; set; }

        static ViewResultFormatters()
        {
            Formatters = new List<IViewResultFormatter>();
            // add default formatters
            Formatters.Add(new JsonViewResultFormatter());
            Formatters.Add(new XmlViewResultFormatter());
        }

        public static IViewResultFormatter GetFormatter(ControllerContext controllerContext)
        {
            foreach (var formatter in Formatters)
            {
                if (formatter.IsSatisfiedBy(controllerContext))
                    return formatter;
            }

            return null;
        }
    }
}
