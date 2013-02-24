using System.Web.Http.Dependencies;

namespace Fabrik.Common.WebAPI
{
    /// <summary>
    /// Extensions for <see cref="System.Web.Http.Dependencies.IDependencyResolver"/>
    /// </summary>
    public static class DependencyResolverExtensions
    {
        public static TService GetService<TService>(this IDependencyResolver dependencyResolver) where TService : class
        {
            return dependencyResolver.GetService(typeof(TService)) as TService;
        }
    }
}
