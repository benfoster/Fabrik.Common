using System.Web;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// A base class for creating testable Http Modules
    /// </summary>
    /// <remarks>
    /// Credit Kazi Manzur Rashid - http://weblogs.asp.net/rashid/archive/2009/03/12/unit-testable-httpmodule-and-httphandler.aspx
    /// </remarks>
    public abstract class HttpModuleBase : IHttpModule
    {
        public virtual void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => OnBeginRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
            context.Error += (sender, e) => OnError(new HttpContextWrapper(((HttpApplication)sender).Context));
            context.EndRequest += (sender, e) => OnEndRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
        }

        public virtual void Dispose()
        {
        }

        public virtual void OnBeginRequest(HttpContextBase context)
        {
        }

        public virtual void OnError(HttpContextBase context)
        {
        }

        public virtual void OnEndRequest(HttpContextBase context)
        {
        }
    }
}
