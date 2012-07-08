using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// Provides a base class for metadata plugins that assign metadata using Attributes
    /// </summary>
    public abstract class AttributeMetadataPlugin<TAttribute> : IMetadataPlugin where TAttribute : Attribute
    {        
        public void AssignMetadata(IEnumerable<Attribute> attributes, ModelMetadata metadata)
        {
            var attribute = attributes.OfType<TAttribute>().FirstOrDefault();
            if (attribute == null)
                return;

            AssignMetadata(attribute, metadata);
        }

        public abstract void AssignMetadata(TAttribute attribute, ModelMetadata metadata);
    }
}
