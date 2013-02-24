using System;

namespace Fabrik.Common
{
    public static class FunctionalExtensions
    {
        /// <summary>
        /// Applies the specified <paramref name="actions"/> to the <paramref name="target"/>.
        /// </summary>
        public static void ApplyTo<T>(this T target, params Action<T>[] actions)
        {
            actions.ForEach(action => action.Invoke(target));
        }
    }
}
