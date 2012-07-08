
namespace Fabrik.Common.Web
{
    /// <summary>
    /// A regex constraint for validating slug parameters
    /// </summary>
    public class SlugRouteConstraint : RegexConstraint
    {       
        /// <summary>
        /// Creates a new <see cref="SlugRouteConstraint"/> instance.
        /// </summary>
        /// <param name="allowSegments">Set to true to allow segments (forward slashes) in your slug.</param>
        public SlugRouteConstraint(bool allowSegments = false) : base(allowSegments ? RegexUtils.SlugWithSegmentsRegex : RegexUtils.SlugRegex)
        {
        }
    }
}
