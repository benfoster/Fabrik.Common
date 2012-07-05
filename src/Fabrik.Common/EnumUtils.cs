using System;

namespace Fabrik.Common
{
    /// <summary>
    /// Utilities for working with <see cref="System.Enum"/> types.
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// Converts the string representation of the name or numeric value of one or 
        /// more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <typeparam name="TEnum">An enumeration type.</typeparam>
        /// <param name="value">A string containing the name or value to convert.</param>
        /// <returns>An object of type <typeparamref name="TEnum"/> whose value is represented by <paramref name="value"/>.</returns>
        public static TEnum ParseFromString<TEnum>(string value)
        {
            Ensure.Argument.NotNullOrEmpty(value, "enumString");
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }
    }
}
