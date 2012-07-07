using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public interface IGlobalFilterRegistry
    {
        void RegisterGlobalFilters(GlobalFilterCollection filters);
    }
}
