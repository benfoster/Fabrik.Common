using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// A pluggable version of <see cref="DataAnnotationsModelMetadataProvider"/>
    /// </summary>
    public class PluggableModelMetaDataProvider : DataAnnotationsModelMetadataProvider
    {
        private readonly IEnumerable<IMetadataPlugin> plugins;

        public PluggableModelMetaDataProvider(IEnumerable<IMetadataPlugin> plugins)
        {
            Ensure.Argument.NotNull(plugins, "plugins");
            this.plugins = plugins;
        }
        
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            plugins.ForEach(p => p.AssignMetadata(attributes, metadata));
            return metadata;
        }
    }
}
