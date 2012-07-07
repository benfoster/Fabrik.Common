using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using Machine.Fakes;
using Machine.Specifications;

namespace Fabrik.Common.Web.Tests
{
    [Subject(typeof(XmlResult), "Creating")]
    public class When_the_object_to_serialize_is_null
    {
        static Exception exception;

        Because of = ()
            => exception = Catch.Exception(() => new XmlResult(null));

        It Should_throw_an_ArgumentNullException = ()
            => exception.ShouldBeOfType<ArgumentNullException>();
    }

    [Subject(typeof(XmlResult), "Created")]
    public class When_the_result_is_created
    {
        static XmlResult result;

        Because of = ()
            => result = new XmlResult(new Contact());

        It Should_set_the_data = ()
            => result.Data.ShouldBeOfType<Contact>();

        It Should_set_the_default_content_type_to_textxml = ()
            => result.ContentType.ShouldEqual("text/xml");

        It Should_set_the_encoding_to_utf8 = ()
            => result.Encoding.ShouldEqual(Encoding.UTF8);
    }

    [Subject(typeof(XmlResult), "Executing")]
    public class When_executing_the_result : WithFakes
    {
        static ControllerContext controllerContext;
        static XmlResult result;

        Establish ctx = () => {
            var httpContext = An<HttpContextBase>();
            var httpResponse = An<HttpResponseBase>();
            httpResponse.WhenToldTo(x => x.Output).Return(new StringWriter());
            httpContext.WhenToldTo(x => x.Response).Return(httpResponse);

            controllerContext = new ControllerContext(httpContext, new RouteData(), An<ControllerBase>());

            result = new XmlResult(new Contact { Name = "Ben Foster", Age = 27 });
        };

        Because of = () => {
            result.ExecuteResult(controllerContext);
        };

        It Should_set_the_response_content_type = ()
            => controllerContext.HttpContext.Response.ContentType.ShouldEqual("text/xml");

        It Should_serialize_the_object_to_xml = () =>
        {
            var doc = new XmlDocument();
            doc.LoadXml(controllerContext.HttpContext.Response.Output.ToString());
            doc.SelectSingleNode("/Contact/Name").InnerText.ShouldEqual("Ben Foster");
            doc.SelectSingleNode("/Contact/Age").InnerText.ShouldEqual("27");
        };
    }

    public class Contact
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
