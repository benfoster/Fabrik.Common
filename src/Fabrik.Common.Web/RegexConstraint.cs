using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// For performance gains use this in place of string based Regex constraints. See http://samsaffron.com/archive/2011/10/13/optimising-asp-net-mvc3-routing
    /// </summary>
    public class RegexConstraint : IRouteConstraint 
    {
        Regex regex;

        public RegexConstraint(string pattern, RegexOptions options = RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase)
            : this(new Regex(pattern, options)) {} 

        public RegexConstraint(Regex regex)
        {
            Ensure.Argument.NotNull(regex, "regex");
            this.regex = regex;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            object val;
            values.TryGetValue(parameterName, out val);
            string input = Convert.ToString(val, CultureInfo.InvariantCulture);
            return regex.IsMatch(input);
        }
    }
}
