using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Machine.Fakes;
using Machine.Specifications;

namespace Fabrik.Common.Web.Specs
{
    [Subject(typeof(HtmlHelperExtensions))]
    public class CheckBoxListFor : WithFakes
    {
        static HtmlHelper html;
        static IHtmlString result;
        static IEnumerable<SelectListItem> selectList;
               
        public class When_provided_a_list_of_values
        {
            Establish ctx = () =>
            {
                html = new HtmlHelper(An<ViewContext>(), An<IViewDataContainer>());
                selectList = new SelectList(new[] { "One", "Two", "Three" });
            };

            Because of = ()
                => result = html.CheckBoxList("test", selectList);
            
            It Should_render_a_checkbox_input_for_each_value_in_the_list = ()
                => result.ToHtmlString().ShouldEqual(
                    @"<div>
<label class=""checkbox""><input name=""test"" type=""checkbox"" value=""One"" />One</label>
<label class=""checkbox""><input name=""test"" type=""checkbox"" value=""Two"" />Two</label>
<label class=""checkbox""><input name=""test"" type=""checkbox"" value=""Three"" />Three</label></div>".Replace("\r\n", "")
                );
        }

        public class When_provided_a_list_of_values_with_selected_items
        {
            Establish ctx = () =>
            {
                html = new HtmlHelper(An<ViewContext>(), An<IViewDataContainer>());
                selectList = new MultiSelectList(new[] { "One", "Two", "Three" }, new[] { "One", "Three" });
            };

            Because of = ()
                => result = html.CheckBoxList("test", selectList);

            It Should_mark_the_checkboxes_as_checked = ()
                => result.ToHtmlString().ShouldEqual(
                    @"<div>
<label class=""checkbox""><input checked=""checked"" name=""test"" type=""checkbox"" value=""One"" />One</label>
<label class=""checkbox""><input name=""test"" type=""checkbox"" value=""Two"" />Two</label>
<label class=""checkbox""><input checked=""checked"" name=""test"" type=""checkbox"" value=""Three"" />Three</label></div>".Replace("\r\n", "")
                );
        }
    }
}
