using System;
using System.Collections.Generic;
using System.Linq;

namespace Fabrik.Common
{
    /// <summary>
    /// A Base36 Encoder and Decoder
    /// </summary>
    public static class Base36
    {
        private const string Base36Characters = "0123456789abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Encode the given number into a Base36 string
        /// </summary>
        /// <param name="input">The number to encode.</param>
        public static string Encode(long input)
        {
            Ensure.Argument.IsNot(input < 0, "Input cannot be negative.");

            char[] clistarr = Base36Characters.ToCharArray();
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(clistarr[input % 36]);
                input /= 36;
            }
            return new String(result.ToArray());
        }

        /// <summary>
        /// Decode the Base36 Encoded string into a number
        /// </summary>
        /// <param name="input">The number to decode.</param>
        public static Int64 Decode(string input)
        {
            var reversed = input.ToLower().Reverse();
            long result = 0;
            int pos = 0;
            foreach (char c in reversed)
            {
                result += Base36Characters.IndexOf(c) * (long)Math.Pow(36, pos);
                pos++;
            }
            return result;
        }
    }
}
