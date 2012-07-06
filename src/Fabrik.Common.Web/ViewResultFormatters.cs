using System.Collections.Generic;
using System.Web;

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

        public static IViewResultFormatter GetFormatter(HttpContextBase httpContext)
        {
            foreach (var formatter in Formatters)
            {
                if (formatter.IsSatisfiedBy(httpContext))
                    return formatter;
            }

            return null;
        }
    }
}
