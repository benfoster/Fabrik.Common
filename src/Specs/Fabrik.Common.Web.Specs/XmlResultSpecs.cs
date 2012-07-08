using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using Machine.Fakes;
using Machine.Specifications;

namespace Fabrik.Common.Web.Specs
{
    [Subject(typeof(XmlResult), "Creating the result")]
    public class Creating_the_result
    {
        public class When_the_object_to_serialize_is_null
        {
            static Exception exception;

            Because of = ()
                => exception = Catch.Exception(() => new XmlResult(null));

            It Should_throw_an_ArgumentNullException = ()
                => exception.ShouldBeOfType<ArgumentNullException>();
        }

        public class When_the_object_to_serialize_is_valid
        {
            static XmlResult result;

            Because of = ()
                => result = new XmlResult(new Contact());

            It Should_set_the_data = ()
                => result.Data.ShouldBeOfType<Contact>();

            It Should_set_the_default_content_type_to_textxml = ()
                => result.ContentType.ShouldEqual("text/xml");

            It Should_set_the_encoding_to_UTF8 = ()
                => result.Encoding.ShouldEqual(Encoding.UTF8);
        }
    }

    [Subject(typeof(XmlResult), "Executing the result")]
    public class When_executing_the_result : WithFakes
    {
        static ControllerContext controllerContext;
        static XmlResult result;

        Establish ctx = () => {
            controllerContext = new ControllerContext(DynamicHttpContext.Create(), new RouteData(), An<ControllerBase>());
            result = new XmlResult(new Contact { Name = "Ben Foster", Age = 27 });
        };

        Because of = () => {
            result.ExecuteResult(controllerContext);
        };

        It Should_set_the_response_content_type = ()
            => controllerContext.HttpContext.Response.ContentType.ShouldEqual("text/xml");

        It Should_serialize_the_data_to_xml = () =>
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
