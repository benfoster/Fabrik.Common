
namespace Fabrik.Common.Web
{
    /// <summary>
    /// A regex constraint for validating parameters are numbers greater than zero.
    /// </summary>
    /// <remarks>
    /// Useful for page numbers, identifiers etc.
    /// </remarks>
    public class NumberRouteConstraint : RegexConstraint
    {
        public NumberRouteConstraint() : base(RegexUtils.PositiveNumberRegex)
        {

        }
    }
}
