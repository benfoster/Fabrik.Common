using System;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public class DefaultViewFactory : IViewFactory
    {      
        public TView CreateView<TView>()
        {
            var builder = DependencyResolver.Current.GetService<IViewBuilder<TView>>();

            if (builder != null)
                return builder.Build();

            // otherwise create view using reflection
            return Activator.CreateInstance<TView>();
        }

        public TView CreateView<TInput, TView>(TInput input)
        {
            var builder = DependencyResolver.Current.GetService<IViewBuilder<TInput, TView>>();

            if (builder != null)
                return builder.Build(input);

            // otherwise create using reflection
            return (TView)Activator.CreateInstance(typeof(TView), input);
        }
    }
}
