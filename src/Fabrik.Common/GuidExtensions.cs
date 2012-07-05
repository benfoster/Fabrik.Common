using System;

namespace Fabrik.Common
{
    /// <summary>
    /// Utility methods for <see cref="System.Guid"/>.
    /// </summary>
    public static class GuidUtils
    {
        /// <summary>
        /// Generates a sequential Guid.
        /// </summary>
        /// <remarks>
        /// Taken from the NHibernate project, original contribution of Donald Mull.
        /// </remarks>
        public static Guid GenerateComb()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(0x76c, 1, 1);
            DateTime now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            // Convert to a byte array 
            // SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }

        /// <summary>
        /// Generates a 16 character, Guid based string with very little chance of collision. 
        /// <example>3c4ebc5f5f2c4edc</example>.
        /// </summary>
        /// <remarks>Author Mads Kristensen http://madskristensen.net/post/Generate-unique-strings-and-numbers-in-C.aspx</remarks>
        public static string GuidShortcode()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
    }
}
