using System.ComponentModel;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public class DescriptionAttributeMetadataPlugin : AttributeMetadataPlugin<DescriptionAttribute>
    {
        public override void AssignMetadata(DescriptionAttribute attribute, ModelMetadata metadata)
        {
            metadata.Description = (attribute.Description ?? string.Empty).Trim();
        }
    }
}
