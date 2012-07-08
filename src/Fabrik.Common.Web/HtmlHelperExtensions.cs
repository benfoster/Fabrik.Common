using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return new HtmlString(metadata.Description);
        }
    }
}
