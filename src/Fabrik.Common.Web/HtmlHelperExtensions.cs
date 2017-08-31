using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace Fabrik.Common.Web
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns a description for this model property
        /// </summary>
        public static IHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            Ensure.Argument.NotNull(html, "html");
            Ensure.Argument.NotNull(expression, "expression");
            
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return new HtmlString(metadata.Description);
        }

        /// <summary>
        /// A helper for performing conditional IF logic using Razor
        /// </summary>
        public static MvcHtmlString If(this HtmlHelper html, bool condition, string trueString)
        {
            Ensure.Argument.NotNull(html, "html");
            return html.IfElse(condition, h => MvcHtmlString.Create(trueString), h => MvcHtmlString.Empty);
        }

        /// <summary>
        /// A helper for performing conditional IF,ELSE logic using Razor
        /// </summary>
        public static MvcHtmlString IfElse(this HtmlHelper html, bool condition, string trueString, string falseString)
        {
            Ensure.Argument.NotNull(html, "html");
            return html.IfElse(condition, h => MvcHtmlString.Create(trueString), h => MvcHtmlString.Create(falseString));
        }

        /// <summary>
        /// A helper for performing conditional IF logic using Razor
        /// </summary>
        public static MvcHtmlString If(this HtmlHelper html, bool condition, Func<HtmlHelper, MvcHtmlString> action)
        {
            Ensure.Argument.NotNull(html, "html");
            Ensure.Argument.NotNull(action, "action");
            return html.IfElse(condition, action, h => MvcHtmlString.Empty);
        }

        /// <summary>
        /// A helper for performing conditional IF,ELSE logic using Razor
        /// </summary>
        public static MvcHtmlString IfElse(this HtmlHelper html, bool condition, Func<HtmlHelper, MvcHtmlString> trueAction, Func<HtmlHelper, MvcHtmlString> falseAction)
        {
            Ensure.Argument.NotNull(html, "html");
            Ensure.Argument.NotNull(trueAction, "trueAction");
            Ensure.Argument.NotNull(falseAction, "falseAction"); 
            return (condition ? trueAction : falseAction).Invoke(html);
        }

        /// <summary>
        /// A helper for performing conditional IF logic within Razor
        /// </summary>
        public static TResult If<TResult>(this HtmlHelper html, bool condition, Func<TResult> trueFunc)
        {
            return html.IfElse(condition, trueFunc, () => default(TResult));
        }

        /// <summary>
        /// A helper for performing conditional IF ELSE logic within Razor
        /// </summary>
        public static TResult IfElse<TResult>(this HtmlHelper html, bool criteria, Func<TResult> trueFunc, Func<TResult> falseFunc)
        {
            Ensure.Argument.NotNull(trueFunc, "trueFunc");
            Ensure.Argument.NotNull(falseFunc, "falseFunc");
            return criteria ? trueFunc() : falseFunc();
        }

        /// <summary>
        /// Returns an image element for the specified <paramref name="src"/>
        /// </summary>
        public static MvcHtmlString Image(this HtmlHelper html, string src, string alt = "", object htmlAttributes = null)
        {
            Ensure.Argument.NotNull(html, "html");
            Ensure.Argument.NotNullOrEmpty(src, "src");

            if (src.StartsWith("~"))
                src = VirtualPathUtility.ToAbsolute(src);
            
            var tb = new TagBuilder("img");
            tb.MergeAttribute("src", src);
            tb.MergeAttribute("alt", alt);
            tb.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Creates a dropdown list for an Enum property
        /// </summary>
        /// <exception cref="System.ArgumentException">If the property type is not a valid Enum</exception>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression, string emptyItemText = "", string emptyItemValue = "", object htmlAttributes = null)
        {     
            return html.DropDownListFor(expression, GetEnumSelectList<TEnum>(true, emptyItemText, emptyItemValue), htmlAttributes);
        }

        /// <summary>
        /// Creates a checkbox list for an Enum property
        /// </summary>
        public static MvcHtmlString EnumCheckBoxListFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, IEnumerable<TEnum>>> expression, object htmlAttributes = null)
        {
            return html.CheckBoxListFor(expression, GetEnumSelectList<TEnum>(), htmlAttributes);
        }

        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString CheckBoxListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var listName = ExpressionHelper.GetExpressionText(expression);
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            items = GetCheckboxListWithDefaultValues(metaData.Model, items);
            return htmlHelper.CheckBoxList(listName, items, htmlAttributes);
        }

        /// <summary>
        /// Returns a radio button for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString RadioButtonListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var listName = ExpressionHelper.GetExpressionText(expression);
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            items = GetCheckboxListWithDefaultValues(metaData.Model, items);
            return htmlHelper.RadioButtonList(listName, items, htmlAttributes);
        }

        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string listName, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var container = new TagBuilder("div");
            foreach (var item in items)
            {
                var label = new TagBuilder("label");
                label.MergeAttribute("class", "checkbox"); // default class
                label.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

                var cb = new TagBuilder("input");
                cb.MergeAttribute("type", "checkbox");
                cb.MergeAttribute("name", listName);
                cb.MergeAttribute("value", item.Value ?? item.Text);
                if (item.Selected)
                    cb.MergeAttribute("checked", "checked");

                label.InnerHtml = cb.ToString(TagRenderMode.SelfClosing) + item.Text;

                container.InnerHtml += label.ToString();
            }

            return new MvcHtmlString(container.ToString());
        }

        /// <summary>
        /// Returns a radio button for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string listName, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var container = new TagBuilder("div");
            foreach (var item in items)
            {
                var label = new TagBuilder("label");
                label.MergeAttribute("class", "radio"); // default class
                label.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

                var cb = new TagBuilder("input");
                cb.MergeAttribute("type", "radio");
                cb.MergeAttribute("name", listName);
                cb.MergeAttribute("value", item.Value ?? item.Text);
                if (item.Selected)
                {
                    cb.MergeAttribute("checked", "checked");
                }

                label.InnerHtml = cb.ToString(TagRenderMode.SelfClosing) + item.Text;

                container.InnerHtml += label.ToString();
            }

            return new MvcHtmlString(container.ToString());
        }

        /// <summary>
        /// A helper for wiring up Twitter bootstrap's Typeahead component.
        /// </summary>
        /// <param name="items">The list to be serialized as JSON and assigned to the data-items attribute of the textbox</param>
        public static MvcHtmlString TypeaheadFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<string> source, int items = 8)
        {
            Ensure.Argument.NotNull(expression, "expression");
            Ensure.Argument.NotNull(source, "source");

            var jsonString = new JavaScriptSerializer().Serialize(source);

            return htmlHelper.TextBoxFor(expression, new { autocomplete = "off", data_provide = "typeahead", data_items = items, data_source = jsonString });
        }

        /// <summary>
        /// Renders an Alert if one exists in TempData (requires a partial view named _Alert)
        /// </summary>
        public static MvcHtmlString Alert(this HtmlHelper html)
        {
            var alert = html.ViewContext.TempData[typeof(Alert).FullName] as Alert;
            if (alert != null)
                return html.Partial("_Alert", alert);

            return MvcHtmlString.Empty;
        }

        private static IEnumerable<SelectListItem> GetEnumSelectList<TEnum>(bool addEmptyItemForNullable = false, string emptyItemText = "", string emptyItemValue = "")
        {
            var enumType = typeof(TEnum);
            var nullable = false;

            if (!enumType.IsEnum)
            {
                enumType = Nullable.GetUnderlyingType(enumType);

                if (enumType != null && enumType.IsEnum)
                {
                    nullable = true;
                }
                else 
                {
                    throw new ArgumentException("Not a valid Enum type");
                }
            }
            
            var selectListItems = (from v in Enum.GetValues(enumType).Cast<TEnum>()
                                   select new SelectListItem
                                   {
                                       Text = v.ToString().SeparatePascalCase(),
                                       Value = v.ToString()
                                   }).ToList();

            if (nullable && addEmptyItemForNullable)
            {
                selectListItems.Insert(0, new SelectListItem { Text = emptyItemText, Value = emptyItemValue });
            }

            return selectListItems;
        }

        private static IEnumerable<SelectListItem> GetCheckboxListWithDefaultValues(object defaultValues, IEnumerable<SelectListItem> selectList)
        {
            var defaultValuesList = defaultValues as IEnumerable;

            if (defaultValuesList == null)
                return selectList;

            IEnumerable<string> values = from object value in defaultValuesList
                                         select Convert.ToString(value, CultureInfo.CurrentCulture);

            var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            var newSelectList = new List<SelectListItem>();

            selectList.ForEach(item =>
            {
                item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                newSelectList.Add(item);
            });

            return newSelectList;
        }
    }
}
