using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace Fabrik.Common.Web.StructureMap
{
    /// <summary>
    /// StructureMap implementation of <see cref="System.Web.Mvc.IDependencyResolver"/>
    /// </summary>
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer container;

        public StructureMapDependencyResolver(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                return null;

            return serviceType.IsAbstract || serviceType.IsInterface
                     ? container.TryGetInstance(serviceType)
                     : container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }
    }
}
