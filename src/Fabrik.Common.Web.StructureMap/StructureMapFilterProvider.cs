using System.Collections.Generic;
using System.Web.Mvc;
using StructureMap;

namespace Fabrik.Common.Web.StructureMap
{
    public class StructureMapFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IContainer container;

        public StructureMapFilterProvider(IContainer container)
        {
            this.container = container;
        }

        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);
            filters.ForEach(filter => container.BuildUp(filter.Instance));
            return filters;
        }
    }
}
