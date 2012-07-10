
namespace Fabrik.Common.Web
{
    public interface IViewFactory
    {
        TView CreateView<TView>();
        TView CreateView<TInput, TView>(TInput input);
    }
}
