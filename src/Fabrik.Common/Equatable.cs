using System;

namespace Fabrik.Common
{
    /// <summary>
    /// Provides a base class to make implementing <see cref="System.IEquatable{T}"/> easier.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public abstract class Equatable<T> : IEquatable<T> where T : class, IEquatable<T>
    {
        public abstract override int GetHashCode();

        public virtual bool Equals(T other)
        {
            if (other == null)
                return false;

            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public static bool operator ==(Equatable<T> lhs, Equatable<T> rhs)
        {
            if ((object)lhs == null || (object)rhs == null)
                return object.Equals(lhs, rhs);

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Equatable<T> lhs, Equatable<T> rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Generates a hash code using the given properties (credit Jon Skeet @ http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode)
        /// </summary>
        protected static int GenerateHashCode(params object[] properties)
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;

                foreach (var prop in properties)
                {
                    if (prop != null)
                        hash = hash * 23 + prop.GetHashCode();
                }
                return hash;
            }
        }
    }
}
