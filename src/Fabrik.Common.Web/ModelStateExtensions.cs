using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Converts the <paramref name="modelState"/> to a dictionary that can be easily serialized.
        /// </summary>
        public static IDictionary<string, string[]> ToSerializableDictionary(this ModelStateDictionary modelState)
        {
            return modelState.Where(x => x.Value.Errors.Any()).ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
        }
    }
}
