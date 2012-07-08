using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An interface for metadata plugins.
    /// </summary>
    public interface IMetadataPlugin
    {
        void AssignMetadata(IEnumerable<Attribute> attributes, ModelMetadata metadata);
    }
}
