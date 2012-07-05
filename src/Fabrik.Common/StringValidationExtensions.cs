using System.Text.RegularExpressions;

namespace Fabrik.Common
{
    /// <summary>
    /// Validation extensions for <see cref="System.String"/>
    /// </summary>
    public static class StringValidationExtensions
    {
        /// <summary>
        /// Validates whether the provided <param name="value">string</param> is a valid slug.
        /// </summary>
        public static bool IsValidSlug(this string value)
        {
            return Match(value, RegexUtils.SlugRegex);
        }

        /// <summary>
        /// Validates whether the provided <param name="value">string</param> is a valid (absolute) URL.
        /// </summary>
        public static bool IsValidUrl(this string value) // absolute
        {
            return Match(value, RegexUtils.UrlRegex);
        }

        /// <summary>
        /// Validates whether the provided <param name="value">string</param> is a valid Email Address.
        /// </summary>
        public static bool IsValidEmail(this string value)
        {
            return Match(value, RegexUtils.EmailRegex);
        }

        /// <summary>
        /// Validates whether the provided <param name="value">string</param> is a valid IP Address.
        /// </summary>
        public static bool IsValidIPAddress(this string value)
        {
            return Match(value, RegexUtils.IPAddressRegex);
        }

        private static bool Match(string value, Regex regex)
        {
            return value.IsNotNullOrEmpty() || regex.IsMatch(value);
        }
    }
}
