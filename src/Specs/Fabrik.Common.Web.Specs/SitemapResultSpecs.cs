using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Fabrik.Common.Web.Specs
{
    public class SitemapResultSpecs
    {
        [Subject(typeof(SitemapResult))]
        public class When_executing_the_result : WithFakes
        {
            static ControllerContext controllerContext;
            static SitemapResult result;
            
            Establish ctx = () =>
            {
                controllerContext = new ControllerContext(DynamicHttpContext.Create(), new RouteData(), An<ControllerBase>());
                
                var items = new List<SitemapItem> {
                    new SitemapItem("/posts/azure-virtual-machines-do-not-come-with-a-sysadmin", DateTime.UtcNow, SitemapChangeFrequency.Always, 1.0),
                    new SitemapItem("/posts/injecting-page-metadata-in-aspnet-mvc", changeFrequency: SitemapChangeFrequency.Monthly)
                };
                
                result = new SitemapResult(items);
            };

            Because of = () =>
                result.ExecuteResult(controllerContext);

            It Should_set_the_response_content_type = ()
                => controllerContext.HttpContext.Response.ContentType.ShouldEqual("text/xml");

            It Should_generate_a_valid_xml_sitemap = () =>
            {
                var xml = controllerContext.HttpContext.Response.Output.ToString();

                using (var s = new StringReader(xml))
                {
                    using (var fs = File.OpenRead("sitemap.xsd"))
                    {
                        using (var reader = XmlReader.Create(fs))
                        {
                            var schemas = new XmlSchemaSet();
                            schemas.Add("http://www.sitemaps.org/schemas/sitemap/0.9", reader);

                            bool isValid = true;

                            var doc = XDocument.Load(s);
                            doc.Validate(schemas, (sender, e) =>
                            {
                                isValid = false;
                            });

                            isValid.ShouldBeTrue();
                        }
                    }
                }               
            };
        }
    }
}
