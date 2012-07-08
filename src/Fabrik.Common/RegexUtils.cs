using System.Text.RegularExpressions;

namespace Fabrik.Common
{
    public static class RegexUtils
    {
        /// <summary>
        /// A regular expression for validating slugs.
        /// Does not allow leading or trailing hypens or whitespace
        /// </summary>
        public static readonly Regex SlugRegex = new Regex(@"(^[a-z0-9])([a-z0-9-]+)*([a-z0-9])$");

        /// <summary>
        /// A regular expression for validating slugs with segments
        /// Does not allow leading or trailing hypens or whitespace
        /// </summary>
        public static readonly Regex SlugWithSegmentsRegex = new Regex(@"(^[a-z0-9])(/?[a-z0-9-]+)*([a-z0-9])$");

        /// <summary>
        /// A regular expression for validating IPAddresses. Taken from http://net.tutsplus.com/tutorials/other/8-regular-expressions-you-should-know/
        /// </summary>
        public static readonly Regex IPAddressRegex = new Regex(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

        /// <summary>
        /// A regular expression for validating Email Addresses. Taken from http://net.tutsplus.com/tutorials/other/8-regular-expressions-you-should-know/
        /// </summary>
        public static readonly Regex EmailRegex = new Regex(@"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$");

        /// <summary>
        /// A regular expression for validating absolute Urls. Taken from http://net.tutsplus.com/tutorials/other/8-regular-expressions-you-should-know/
        /// </summary>
        public static readonly Regex UrlRegex = new Regex(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$");

        /// <summary>
        /// A regular expression for validating that string is a positive number GREATER THAN zero.
        /// </summary>
        public static readonly Regex PositiveNumberRegex = new Regex(@"^[1-9]+[0-9]*$");
    }
}
